using System;
using System.Threading;
using System.Threading.Tasks;

namespace Speculative_Selection
{
	public class SpeculativeSelection
	{
		public static void Compute<TInput, TOutput>(
			TInput value,
			Action<long, TOutput> callback,
			params Func<TInput, TOutput>[] functions)
		{
			// Define a counter to indicate the results produced
			int resultCounter = 0;

			// Start a task to perform the parallel loop, otherwise this method block until a result has been found
			// and the functions running at that time have finished, even if they are unsuccessful
			Task.Factory.StartNew(() =>
			{
				// Perform the parallel foreach
				Parallel.ForEach(functions,
					(Func<TInput, TOutput> func, ParallelLoopState loopState, long iterationIndex) =>
					{
						// Compute the result
						TOutput localResult = func(value);

						// Increment the counter
						if (Interlocked.Increment(ref resultCounter) == 1)
						{
							// We are at the first iteration to produce the result
							// Stop the loop
							loopState.Stop();

							// Invoke the callback
							callback(iterationIndex, localResult);
						}
					});
			});
		}
	}
}
