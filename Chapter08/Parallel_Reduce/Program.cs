using System;
using System.Linq;

namespace Parallel_Reduce
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create some source data
			int[] sourceData = Enumerable.Range(0, 10).ToArray();

			// Create the aggregation function
			Func<int, int, int> reduceFunction = (value1, value2) => value1 + value2;

			// Perform the reduction
			int result = ParallelReduce.Reduce(sourceData, 0, reduceFunction);

			// Write out the result
			Console.WriteLine("Result: {0}", result);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
