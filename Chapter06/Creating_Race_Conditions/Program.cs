using System;
using System.Linq;

namespace Creating_Race_Conditions
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

			// Create a counter - this will be shared
			int counter = 1000;

			// Create a PLINQ query that uses the shared value
			var result = from e in sourceData.AsParallel()
						 where (counter-- > 0)
						 select e;

			Console.WriteLine("Expected {0} items", counter);
			Console.WriteLine("Got {0} items", result.ToArray().Length);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
