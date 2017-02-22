using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing02
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the source data
			int[] sourceData = new int[10];
			for (int i = 0; i < sourceData.Length; i++)
			{
				sourceData[i] = i + 1;
			}

			// Define a sequential linq query
			IEnumerable<double> results1 = from item in sourceData
										   select Math.Pow(item, 2);

			// Enumerate the results of the sequential query
			foreach (var item in results1)
			{
				Console.WriteLine("Sequential result: {0}", item);
			}

			// Define a parallel linq query
			IEnumerable<double> results2 = from item in sourceData.AsParallel()
										   select Math.Pow(item, 2);

			// Enumerate the results of the parallel query
			foreach (var item in results2)
			{
				Console.WriteLine("Parallel result: {0}", item);
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
