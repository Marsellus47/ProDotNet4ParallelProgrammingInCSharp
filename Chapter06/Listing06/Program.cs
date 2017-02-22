using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing06
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the source data
			int[] sourceData = new int[5];
			for (int i = 0; i < sourceData.Length; i++)
			{
				sourceData[i] = i + 1;
			}

			// Preserve order with the AsOrdered() method
			IEnumerable<double> results1 = from item in sourceData.AsParallel().AsOrdered()
										   select Math.Pow(item, 2);

			// Create an index into the source array
			int index = 1;

			// Enumerate the results
			foreach (var item in results1)
			{
				Console.WriteLine("Bad result {0} from item {1}", item, index++);
			}

			// Perform the query without ordering the results
			var results2 = from item in sourceData.AsParallel()
						   select new
						   {
							   sourceValue = item,
							   resultValue = Math.Pow(item, 2)
						   };

			// Enumerate the results
			foreach (var item in results2)
			{
				Console.WriteLine("Better result {0} from item {1}",
					item.resultValue, item.sourceValue);
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
