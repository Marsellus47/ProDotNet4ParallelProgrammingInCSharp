using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Listing17
{
	public class StaticPartitioner<T> : Partitioner<T>
	{
		private T[] data;

		public StaticPartitioner(T[] data)
		{
			this.data = data;
		}

		public override bool SupportsDynamicPartitions
		{
			get { return false; }
		}

		public override IList<IEnumerator<T>> GetPartitions(int partitionCount)
		{
			// Create the list to hold the enumerators
			IList<IEnumerator<T>> list = new List<IEnumerator<T>>();

			// Determine how many items per enumerator
			int itemsPerEnum = data.Length / partitionCount;

			// Process all but the last partition
			for (int i = 0; i < partitionCount - 1; i++)
			{
				list.Add(CreateEnum(i * itemsPerEnum, (i + 1) * itemsPerEnum));
			}

			// Handle the last, potentially irregularly sized, partition
			list.Add(CreateEnum((partitionCount - 1) * itemsPerEnum, data.Length));

			// Return the list as the result
			return list;
		}

		private IEnumerator<T> CreateEnum(int startIndex, int endIndex)
		{
			int index = startIndex;
			while (index < endIndex)
			{
				yield return data[index++];
			}
		}
	}
}
