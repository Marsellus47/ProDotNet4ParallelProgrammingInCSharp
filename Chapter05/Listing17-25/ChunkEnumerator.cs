using System;
using System.Collections.Generic;

namespace Listing17_25
{
	class ChunkEnumerator
	{
		private readonly ContextPartitioner parentPartitioner;

		public ChunkEnumerator(ContextPartitioner parent)
		{
			parentPartitioner = parent;
		}

		public IEnumerator<WorkItem> GetEnumerator()
		{
			while(true)
			{
				// Get the indices of the next chunk
				Tuple<long, long> chunkIndices = parentPartitioner.GetNextChunk();

				// Check that we have data to deliver
				if(chunkIndices.Item1 == -1 && chunkIndices.Item2 == -1)
				{
					// There is no more data
					break;
				}
				else
				{
					// Enter a loop to yield the data items
					for (long i = chunkIndices.Item1; i < chunkIndices.Item2; i++)
					{
						yield return parentPartitioner.dataItems[i];
					}
				}
			}
		}
	}
}
