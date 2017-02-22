using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing01
{
	class Program
	{
		static void Main(string[] args)
		{
			int[] sourceData = new int[100];
			for (int i = 0; i < sourceData.Length; i++)
			{
				sourceData[i] = i + 1;
			}

			IEnumerable<int> results = from item in sourceData.AsParallel()
									   where item % 2 == 0
									   select item;

			foreach (var item in results)
			{
				Console.WriteLine("Item {0}", item);
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
