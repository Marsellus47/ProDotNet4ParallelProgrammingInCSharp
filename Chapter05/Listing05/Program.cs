using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Listing05
{
	class Program
	{
		static void Main(string[] args)
		{
			Parallel.ForEach(SteppedIterator(0, 10, 2), index =>
			{
				Console.WriteLine("Index value: {0}", index);
			});

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}

		static IEnumerable<int> SteppedIterator(int startIndex, int endIndex, int stepSize)
		{
			for (int i = startIndex; i < endIndex; i += stepSize)
			{
				yield return i;
			}
		}
	}
}
