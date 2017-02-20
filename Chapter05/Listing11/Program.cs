using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing11
{
	class Program
	{
		static void Main(string[] args)
		{
			int total = 0;

			Parallel.For(
				0,
				100,
				() => 0,
				(int index, ParallelLoopState loopState, int tlsValue) =>
				{
					tlsValue += index;
					return tlsValue;
				},
				value => Interlocked.Add(ref total, value));

			Console.WriteLine("Total: {0}", total);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
