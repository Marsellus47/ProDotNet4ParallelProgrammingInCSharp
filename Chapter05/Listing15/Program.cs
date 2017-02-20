using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Listing15
{
	class Program
	{
		static void Main(string[] args)
		{
			double[] resultData = new double[100000000];
			const int partitionSize = 100000;
			Stopwatch stopwatch = new Stopwatch();

			// Created a partitioner that will chunk the data
			OrderablePartitioner<Tuple<int, int>> chunkPart = Partitioner.Create(0, resultData.Length, partitionSize);

			// Perform the loop in chunks
			stopwatch.Start();
			Parallel.ForEach(chunkPart, (Tuple<int, int> chunkRange) =>
			{
				// Iterate through all of the values in the chunk range
				for (int i = chunkRange.Item1; i < chunkRange.Item2; i++)
				{
					resultData[i] = Math.Pow(i, 2);
				}
			});
			stopwatch.Stop();
			Console.WriteLine("Loop takes {0} seconds", stopwatch.Elapsed.TotalSeconds);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
