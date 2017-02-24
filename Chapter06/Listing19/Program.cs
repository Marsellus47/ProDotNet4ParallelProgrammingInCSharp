using System;
using System.Linq;

namespace Listing19
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create some source data
			int[] sourceData = new int[10000];
			for (int i = 0; i < sourceData.Length; i++)
			{
				sourceData[i] = i;
			}

			// Perform a custom aggregation
			double aggregateResult = sourceData
				.AsParallel()
				.Aggregate(
				// 1st function - initialize the result
				0.0,
				// 2nd function - process each item and the per-Task total
				(subtotal, item) => subtotal += Math.Pow(item, 2),
				// 3rd function - process the overal total and the per-Task total
				(total, subtotal) => total + subtotal,
				// 4th function - perform final processing
				total => total / 2);

			// Write out the result
			Console.WriteLine("Total: {0}", aggregateResult);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
