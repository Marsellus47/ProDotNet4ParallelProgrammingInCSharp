using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing12
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
				.WithDegreeOfParallelism(2)
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
