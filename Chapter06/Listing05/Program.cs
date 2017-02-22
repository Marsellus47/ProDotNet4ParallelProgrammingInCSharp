using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing05
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

			// Preserve order with the AsOrdered() method
			IEnumerable<double> results = from item in sourceData.AsParallel().AsOrdered()
										  select Math.Pow(item, 2);

			// Enumerate the results of the parallel query
			foreach (var item in results)
			{
				Console.WriteLine("Parallel result: {0}", item);
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
