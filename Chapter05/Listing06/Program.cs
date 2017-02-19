using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing06
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a ParallelOptions instance and set the max concurrency to 1
			ParallelOptions options = new ParallelOptions { MaxDegreeOfParallelism = 1 };

			// Perform a parallel for loop
			Parallel.For(0, 10, options, index =>
			{
				Console.WriteLine("For Index {0} started", index);
				Thread.Sleep(500);
				Console.WriteLine("For Index {0} finished", index);
			});

			// Create an array of ints to process
			int[] dataElements = new int[] { 0, 2, 4, 6, 8 };

			// Perform a parallel foreach loop
			Parallel.ForEach(dataElements, options, item =>
			{
				Console.WriteLine("ForEach Index {0} started", item);
				Thread.Sleep(500);
				Console.WriteLine("ForEach Index {0} finished", item);
			});

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
