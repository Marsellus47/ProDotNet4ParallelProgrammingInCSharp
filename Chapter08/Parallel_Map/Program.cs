using System;
using System.Linq;

namespace Parallel_Map
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the source data
			int[] sourceData = Enumerable.Range(0, 100).ToArray();

			// Define the mapping function
			Func<int, double> mapFunction = value => Math.Pow(value, 2);

			// Map the source data
			double[] resultData = ParallelMap.Map<int, double>(mapFunction, sourceData);

			// Run through the results
			for (int i = 0; i < sourceData.Length; i++)
			{
				Console.WriteLine("Value {0} mapped to {1}",
					sourceData[i], resultData[i]);
			}
		}
	}
}
