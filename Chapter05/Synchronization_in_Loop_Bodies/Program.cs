using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Synchronization_in_Loop_Bodies
{
	class Program
	{
		static void Main(string[] args)
		{
			Stopwatch stopwatch = new Stopwatch();
			int numberOfItems = 1000000;

			// Create the shared data value
			long total = 0;

			// Create the lock object
			object lockObj = new object();

			// BAD: Perform a parallel loop
			stopwatch.Start();
			Parallel.For(
				0,
				numberOfItems,
				item =>
				{
					// Get the lock on the shared value
					lock(lockObj)
					{
						// Add the square of the current value to the running total
						total += (long)Math.Pow(item, 2);
					}
				});
			stopwatch.Stop();
			Console.WriteLine("Elapsed {0} milliseconds", stopwatch.ElapsedMilliseconds);
			Console.WriteLine("Expected result: 333332833333500000");
			Console.WriteLine("Actual result:   {0}", total);

			// GOOD: Perform a parallel loop
			total = 0;
			stopwatch.Restart();
			Parallel.For(
				0,
				numberOfItems,
				() => 0,
				(long item, ParallelLoopState loopState, long tlsValue) =>
				{
					tlsValue += (long)Math.Pow(item, 2);
					return tlsValue;
				},
				value => Interlocked.Add(ref total, value));
			stopwatch.Stop();
			Console.WriteLine("Elapsed {0} milliseconds", stopwatch.ElapsedMilliseconds);
			Console.WriteLine("Expected result: 333332833333500000");
			Console.WriteLine("Actual result:   {0}", total);
		}
	}
}
