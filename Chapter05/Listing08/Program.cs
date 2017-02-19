using System;
using System.Threading.Tasks;

namespace Listing08
{
	class Program
	{
		static void Main(string[] args)
		{
			ParallelLoopResult result = Parallel.For(0, 100, (int index, ParallelLoopState loopState) =>
			{
				// Calculate the square of the index
				double sqr = Math.Pow(index, 2);

				// If the square value is > 100 then break
				if(sqr > 100)
				{
					Console.WriteLine("Breaking on index {0}", index);
					loopState.Break();
				}
				else
				{
					// Write out the value
					Console.WriteLine("Square value of {0} is {1}", index, sqr);
				}
			});

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
