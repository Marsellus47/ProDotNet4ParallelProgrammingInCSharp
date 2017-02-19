using System;
using System.Threading.Tasks;

namespace Listing09
{
	class Program
	{
		static void Main(string[] args)
		{
			// Run a parallel loop in which one of the iterations calls Stop()
			ParallelLoopResult loopResult = Parallel.For(0, 10, (index, loopState) =>
			{
				if (index == 2)
				{
					loopState.Stop();
				}
			});

			// Get the details from the loop result
			Console.WriteLine("Loop Result");
			Console.WriteLine("IsCompleted: {0}", loopResult.IsCompleted);
			Console.WriteLine("BreakValue: {0}", loopResult.LowestBreakIteration.HasValue);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
