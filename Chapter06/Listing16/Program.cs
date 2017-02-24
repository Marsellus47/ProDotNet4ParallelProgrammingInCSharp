using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing16
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create some source data
			int[] sourceData = new int[5];
			for (int i = 0; i < sourceData.Length; i++)
			{
				sourceData[i] = i;
			}

			// Define a fully buffered query
			IEnumerable<double> results = sourceData
				.AsParallel()
				.WithMergeOptions(ParallelMergeOptions.FullyBuffered)
				.Select(item =>
				{
					double resultItem = Math.Pow(item, 2);
					Console.WriteLine("Produced result {0}", resultItem);
					return resultItem;
				});

			// Enumerate the query results
			foreach (var item in results)
			{
				Console.WriteLine("Enumeration got result {0}", item);
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
