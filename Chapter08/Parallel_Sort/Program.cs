using System;
using System.Linq;

namespace Parallel_Sort
{
	class Program
	{
		static void Main(string[] args)
		{
			// Generate some random source data
			Random random = new Random();
			int[] sourceData = new int[5000000];
			for (int i = 0; i < sourceData.Length; i++)
			{
				sourceData[i] = random.Next(1, 100);
			}

			// Perform the parallel sort
			ParallelSort<int>.ParallelQuickSort(sourceData, new IntComparer());

			foreach (var item in sourceData.Distinct())
			{
				Console.WriteLine("Item {0}", item);
			}
		}
	}
}
