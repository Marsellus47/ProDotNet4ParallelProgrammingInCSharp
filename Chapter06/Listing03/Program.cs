using System;
using System.Linq;

namespace Listing03
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the source data
			int[] sourceData = new int[10];
			for (int i = 0; i < sourceData.Length; i++)
			{
				sourceData[i] = i;
			}

			// Define a sequential linq query
			var sequentialResults = sourceData.Select(item => Math.Pow(item, 2));

			// Enumerate the results of the sequential query
			foreach (var item in sequentialResults)
			{
				Console.WriteLine("Sequential result: {0}", item);
			}

			// Define a parallel linq query
			var parallelResults = sourceData.AsParallel().Select(item => Math.Pow(item, 2));

			// Enumerate the results of the parallel query
			foreach (var item in parallelResults)
			{
				Console.WriteLine("Parallel result: {0}", item);
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
