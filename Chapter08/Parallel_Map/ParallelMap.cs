using System;
using System.Linq;

namespace Parallel_Map
{
	class ParallelMap
	{
		public static TOutput[] Map<TInput, TOutput>(
			Func<TInput, TOutput> mapFunction,
			TInput[] input)
		{
			return input
				.AsParallel()
				.AsOrdered()
				.Select(value => mapFunction(value))
				.ToArray();
		}
	}
}
