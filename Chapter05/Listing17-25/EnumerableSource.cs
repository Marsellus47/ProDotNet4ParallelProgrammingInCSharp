using System.Collections;
using System.Collections.Generic;

namespace Listing17_25
{
	class EnumerableSource : IEnumerable<WorkItem>
	{
		ContextPartitioner parentPartitioner;

		public EnumerableSource(ContextPartitioner parent)
		{
			parentPartitioner = parent;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<WorkItem>)this).GetEnumerator();
		}

		IEnumerator<WorkItem> IEnumerable<WorkItem>.GetEnumerator()
		{
			return new ChunkEnumerator(parentPartitioner).GetEnumerator();
		}
	}
}
