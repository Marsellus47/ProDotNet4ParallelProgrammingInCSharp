using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Listing17_25
{
	public class ContextPartitioner : Partitioner<WorkItem>
	{
		// The set of data items to partition
		internal WorkItem[] dataItems;

		// The target sum of values per chunk
		protected int targetSum;

		// The first unchunked item
		private long sharedStartIndex = 0;

		// Lock object to avoid index data races
		private object lockObject = new object();

		// The object used to create enumerators
		private EnumerableSource enumSource;

		public ContextPartitioner(WorkItem[] data, int target)
		{
			// Set instance variables from the parameters
			dataItems = data;
			targetSum = target;

			// Create the enumerable source
			enumSource = new EnumerableSource(this);
		}

		public override bool SupportsDynamicPartitions
		{
			get
			{
				// Dynamic partitions are required for parallel foreach loops
				return true;
			}
		}

		public override IList<IEnumerator<WorkItem>> GetPartitions(int partitionCount)
		{
			// Create the list which will be the result
			IList<IEnumerator<WorkItem>> partitionList = new List<IEnumerator<WorkItem>>();

			// Get the IEnumerable that will generate dynamic partitions
			IEnumerable<WorkItem> enumObj = GetDynamicPartitions();

			// Create the required number of partitions
			for (int i = 0; i < partitionCount; i++)
			{
				partitionList.Add(enumObj.GetEnumerator());
			}

			// Return the result
			return partitionList;
		}

		public override IEnumerable<WorkItem> GetDynamicPartitions()
		{
			return enumSource;
		}

		internal Tuple<long, long> GetNextChunk()
		{
			// Create the result tuple
			Tuple<long, long> result;

			// Get an exclusive lock as we perform this operation
			lock(lockObject)
			{
				// Check that ther is still data available
				if(sharedStartIndex < dataItems.Length)
				{
					int sum = 0;
					long endIndex = sharedStartIndex;
					while (endIndex < dataItems.Length && sum < targetSum)
					{
						sum += dataItems[endIndex].WorkDuration;
						endIndex++;
					}
					result = new Tuple<long, long>(sharedStartIndex, endIndex);
					sharedStartIndex = endIndex;
				}
				else
				{
					// There is no data available
					result = new Tuple<long, long>(-1, -1);
				}
			}

			// End of locked region

			// Return the result
			return result;
		}
	}
}
