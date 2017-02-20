using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Listing14
{
	class Program
	{
		delegate void ProcessValue(int value);
		static double[] resultData = new double[100000000];

		static void Main(string[] args)
		{
			Stopwatch stopwatch = new Stopwatch();

			// Perform the parallel loop
			stopwatch.Start();
			Parallel.For(0, resultData.Length, index =>
			{
				// Compute the result for the current index
				resultData[index] = Math.Pow(index, 2);
			});
			stopwatch.Stop();
			Console.WriteLine("Loop takes {0} seconds", stopwatch.Elapsed.TotalSeconds);

			// Perform the loop again, but make the delegate explicit
			stopwatch.Restart();
			Parallel.For(0, resultData.Length, delegate (int index)
			{
				resultData[index] = Math.Pow(index, 2);
			});
			stopwatch.Stop();
			Console.WriteLine("Loop with delegate takes {0} seconds", stopwatch.Elapsed.TotalSeconds);

			// Perform the loop once more, but this time using the declared delegate action
			ProcessValue pdel = new ProcessValue(ComputeResultValue);
			Action<int> paction = new Action<int>(pdel);
			stopwatch.Restart();
			Parallel.For(0, resultData.Length, paction);
			stopwatch.Stop();
			Console.WriteLine("Loop with delegate action takes {0} seconds", stopwatch.Elapsed.TotalSeconds);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}

		private static void ComputeResultValue(int indexValue)
		{
			resultData[indexValue] = Math.Pow(indexValue, 2);
		}
	}
}
