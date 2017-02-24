using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing20
{
	class Program
	{
		static void Main(string[] args)
		{
			// Use PLINQ to process a prallel range
			IEnumerable<double> result1 = from e in ParallelEnumerable.Range(0, 10)
										  where e % 2 == 0
										  select Math.Pow(e, 2);

			foreach (var item in result1)
			{
				Console.WriteLine("Result 1: {0}", item);
			}

			// Use PLINQ to process a parallel repeating sequence
			IEnumerable<double> result2 = ParallelEnumerable.Repeat(10, 20)
				.Select(item => Math.Pow(item, 2));

			foreach (var item in result2)
			{
				Console.WriteLine("Result 2: {0}", item);
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
