using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Listing02
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create some source data
			Random random = new Random();
			int[] sourceData = new int[10000000];
			for (int i = 0; i < sourceData.Length; i++)
			{
				sourceData[i] = random.Next(0, int.MaxValue);
			}

			// Define the measurement variables
			int numberOfIterations = 10;
			int maxDegreeOfConcurrency = 16;

			// Define the lock object for updating the shared result
			object lockObject = new object();

			// Outer loop is degree of concurrency
			for (int concurrency = 1; concurrency <= maxDegreeOfConcurrency; concurrency++)
			{
				// Reset the stopwatch for this degree
				Stopwatch stopwatch = Stopwatch.StartNew();

				// Create the loop options for this degree
				ParallelOptions options = new ParallelOptions { MaxDegreeOfParallelism = concurrency };

				for (int iteration = 0; iteration < numberOfIterations; iteration++)
				{
					// Define the (shared) result
					double result = 0;

					// Perform the work
					Parallel.ForEach(
						sourceData,
						options,
						() => 0.0,
						(int value, ParallelLoopState loopState, long index, double localTotal) =>
						{
							return localTotal + Math.Pow(value, 2);
						},
						localTotal =>
						{
							lock(lockObject)
							{
								result += localTotal;
							}
						});
				}

				// Stop the stopwatch
				stopwatch.Stop();

				// Write out the per-iteration time for this degree of concurrency
				Console.WriteLine("Concurrency {0}: Per-iteration time is {1} ms",
					concurrency, stopwatch.ElapsedMilliseconds / numberOfIterations);
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
