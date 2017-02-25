using System.Collections.Generic;

namespace Parallel_Sort
{
	public class IntComparer : IComparer<int>
	{
		public int Compare(int first, int second)
		{
			return first.CompareTo(second);
		}
	}
}
