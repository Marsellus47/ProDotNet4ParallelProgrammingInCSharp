using System;
using System.Collections.Generic;
using System.Linq;

namespace Parallel_MapReduce
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a function that lists the factors
			Func<int, IEnumerable<int>> map = value =>
			{
				IList<int> factors = new List<int>();
				for (int i = 1; i < value; i++)
				{
					if(value % i == 0)
					{
						factors.Add(i);
					}
				}
				return factors;
			};

			// Create the group function - in this example we want to group
			// the same results together, so we select the value itself
			Func<int, int> group = value => value;

			// Create the reduce function - this simply counts the number of elements in the grouping
			// and returns a Key/Value pair with the result as the key and the count as the value
			Func<IGrouping<int, int>, KeyValuePair<int, int>> reduce =
				grouping => new KeyValuePair<int, int>(grouping.Key, grouping.Count());

			// Create some source data
			IEnumerable<int> sourceData = Enumerable.Range(1, 50);

			// Use parallel map reduce with the source data and the map, group and reduce functions
			IEnumerable<KeyValuePair<int, int>> result = ParallelMapReduce.MapReduce(
				sourceData,
				map,
				group,
				reduce);

			// Process the results
			foreach (var item in result)
			{
				Console.WriteLine("{0} is a factor {1} times",
					item.Key, item.Value);
			}
		}
	}
}
