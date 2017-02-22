using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing04
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the source data
			int[] sourceData = new int[100000];
			for (int i = 0; i < sourceData.Length; i++)
			{
				sourceData[i] = i;
			}

			// Define a filtering query using keywords
			IEnumerable<double> results1 = from item in sourceData.AsParallel()
										   where item % 2 == 0
										   select Math.Pow(item, 2);

			// Enumerate the results
			foreach (var item in results1)
			{
				Console.WriteLine("Result 1: {0}", item);
			}

			// Define a filtering query using extension methods
			IEnumerable<double> results2 = sourceData.AsParallel()
				.Where(item => item % 2 == 0)
				.Select(item => Math.Pow(item, 2));

			// Enumerate the results
			foreach (var item in results2)
			{
				Console.WriteLine("Result 2: {0}", item);
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
