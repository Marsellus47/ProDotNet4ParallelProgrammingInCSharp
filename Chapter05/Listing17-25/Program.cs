using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Listing17_25
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a random number source
			Random random = new Random();

			// Create the source data
			WorkItem[] sourceData = new WorkItem[10000];
			for (int i = 0; i < sourceData.Length; i++)
			{
				sourceData[i] = new WorkItem { WorkDuration = random.Next(1, 11) };
			}

			// Created the contextual partitioner
			Partitioner<WorkItem> cPartitioner = new ContextPartitioner(sourceData, 100);

			// Create the parallel
			Parallel.ForEach(cPartitioner, item =>
			{
				// Perform the work item
				item.PerformWork();
			});

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
