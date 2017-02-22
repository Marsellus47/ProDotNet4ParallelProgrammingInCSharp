using System;
using System.Linq;

namespace Listing08
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the source data
			int[] sourceData = new int[50];
			for (int i = 0; i < sourceData.Length; i++)
			{
				sourceData[i] = i + 1;
			}

			// Filter the data and call ForAll()
			sourceData.AsParallel()
				.Where(item => item % 2 == 0)
				.ForAll(item => Console.WriteLine("Item {0} Result {1}",
					item, Math.Pow(item, 2)));

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
