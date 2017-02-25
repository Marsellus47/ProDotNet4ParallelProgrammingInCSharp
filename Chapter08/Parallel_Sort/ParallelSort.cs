using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parallel_Sort
{
	public class ParallelSort<T>
	{
		public static void ParallelQuickSort(
			T[] data,
			IComparer<T> comparer,
			int maxDepth = 16,
			int minBlockSize = 1000)
		{
			// Call the internal method
			Sort(data, 0, data.Length - 1, comparer, 0, maxDepth, minBlockSize);
		}

		private static void Sort(
			T[] data,
			int startIndex,
			int endIndex,
			IComparer<T> comparer,
			int depth,
			int maxDepth,
			int minBlockSize)
		{
			if(startIndex < endIndex)
			{
				// If we have exceeded the depth threshold or there are
				// fewer items than we would like, then use sequential sort
				if(depth > maxDepth || endIndex - startIndex < minBlockSize)
				{
					Array.Sort(data, startIndex, endIndex - startIndex + 1, comparer);
				}
				else
				{
					// We need to parallelize
					int pivotIndex = PartitionBlock(data, startIndex, endIndex, comparer);

					// Recurse on the left and right blocks
					Task leftTask = Task.Factory.StartNew(() =>
					{
						Sort(data, startIndex, pivotIndex - 1, comparer, depth + 1, maxDepth, minBlockSize);
					});
					Task rightTask = Task.Factory.StartNew(() =>
					{
						Sort(data, pivotIndex + 1, endIndex, comparer, depth + 1, maxDepth, minBlockSize);
					});

					// Wait for the tasks to complete
					Task.WaitAll(leftTask, rightTask);
				}
			}
		}

		private static int PartitionBlock(T[] data, int startIndex, int endIndex, IComparer<T> comparer)
		{
			// Get the pivot value - we will be comparing all of the other items against this value
			T pivot = data[startIndex];

			// Put the pivot value at the end of block
			SwapValues(data, startIndex, endIndex);

			// Index used to store values smaller than the pivot
			int storeIndex = startIndex;

			// Iterate throught the items in the block
			for (int i = startIndex; i < endIndex; i++)
			{
				// Look for items that are smaller or equal to the pivot
				if(comparer.Compare(data[i], pivot) <= 0)
				{
					SwapValues(data, i, storeIndex);
					storeIndex++;
				}
			}
			SwapValues(data, storeIndex, endIndex);
			return storeIndex;
		}

		private static void SwapValues(T[] data, int firstIndex, int secondIndex)
		{
			T holder = data[firstIndex];
			data[firstIndex] = data[secondIndex];
			data[secondIndex] = holder;
		}
	}
}
