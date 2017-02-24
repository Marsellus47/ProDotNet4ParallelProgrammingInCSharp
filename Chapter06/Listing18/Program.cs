using Listing17;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing18
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create some source data
			int[] sourceData = new int[10];
			for (int i = 0; i < sourceData.Length; i++)
			{
				sourceData[i] = i;
			}

			// Create the partitioner
			StaticPartitioner<int> partitioner = new StaticPartitioner<int>(sourceData);

			// Define a query
			IEnumerable<double> results = partitioner
				.AsParallel()
				.Select(item => Math.Pow(item, 2));

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
