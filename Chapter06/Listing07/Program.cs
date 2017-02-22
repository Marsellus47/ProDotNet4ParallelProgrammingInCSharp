using System;
using System.Linq;

namespace Listing07
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the source data
			int[] sourceData = new int[100000];
			for (int i = 0; i < sourceData.Length; i++)
			{
				sourceData[i] = i + 1;
			}

			// Define a query that has an ordered subquery
			var result = sourceData.AsParallel().AsOrdered()
				.Take(10).AsUnordered()
				.Select(item => new
				{
					sourceValue = item,
					resultValue = Math.Pow(item, 2)
				});

			foreach (var item in result)
			{
				Console.WriteLine("Source {0}, Result {1}",
					item.sourceValue, item.resultValue);
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
