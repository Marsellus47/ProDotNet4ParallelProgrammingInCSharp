using System.Collections;
using System.Collections.Generic;

namespace Listing26
{
	class EnumerableSource : IEnumerable<KeyValuePair<long, WorkItem>>
	{
		ContextPartitioner parentPartitioner;

		public EnumerableSource(ContextPartitioner parent)
		{
			parentPartitioner = parent;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<long, WorkItem>>)this).GetEnumerator();
		}

		IEnumerator<KeyValuePair<long, WorkItem>> IEnumerable<KeyValuePair<long, WorkItem>>.GetEnumerator()
		{
			return new ChunkEnumerator(parentPartitioner).GetEnumerator();
		}
	}
}
