using System;
using System.Linq;

namespace Parallel_Reduce
{
	public class ParallelReduce
	{
		public static TValue Reduce<TValue>(
			TValue[] sourceData,
			TValue seedValue,
			Func<TValue, TValue, TValue> reduceFunction)
		{
			// Perform the reduction
			return sourceData
				.AsParallel()
				.Aggregate(
					seedValue,
					(localResult, value) => reduceFunction(localResult, value),
					(overalResult, localResult) => reduceFunction(overalResult, localResult),
					overalResult => overalResult);
		}
	}
}
