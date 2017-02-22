using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing11
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

			// Define the query and force parallelism
			IEnumerable<double> results = sourceData.AsParallel()
				.WithExecutionMode(ParallelExecutionMode.ForceParallelism)
				.Where(item => item % 2 == 0)
				.Select(item => Math.Pow(item, 2));

			foreach (var item in results)
			{
				Console.WriteLine("Result {0}", item);
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
