using System;
using System.Linq;

namespace Sequential_Filtering
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create some source data
			int[] sourceData = new int[100];
			for (int i = 0; i < sourceData.Length; i++)
			{
				sourceData[i] = i;
			}

			// Perform the query - but note where the AsParallel call is
			var result = sourceData
				.Where(item => item % 2 == 0)
				.AsParallel()
				.Select(item => item);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
