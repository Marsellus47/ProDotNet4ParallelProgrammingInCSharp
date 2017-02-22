using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Listing09
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

			Console.WriteLine("Defining PLINQ query");
			// Define the query
			IEnumerable<double> results = sourceData.AsParallel()
				.Select(item =>
				{
					Console.WriteLine("Processing item {0}", item);
					return Math.Pow(item, 2);
				});

			Console.WriteLine("Waiting...");
			Thread.Sleep(5000);

			// Sum the results - this will trigger execution of the query
			Console.WriteLine("Accessing results");
			double total = 0;
			foreach (var item in results)
			{
				total += item;
			}
			Console.WriteLine("Total {0}", total);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
