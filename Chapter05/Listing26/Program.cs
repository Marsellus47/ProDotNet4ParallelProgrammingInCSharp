using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listing26
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

			// Create the result data array
			WorkItem[] resultData = new WorkItem[sourceData.Length];

			// Created the contextual partitioner
			OrderablePartitioner<WorkItem> cPartitioner = new ContextPartitioner(sourceData, 100);

			// Create the parallel
			Parallel.ForEach(cPartitioner, (item, loopState, index) =>
			{
				// Perform the work item
				item.PerformWork();

				// Place the work item in the result array
				resultData[index] = item;
			});

			// Compare the source items to the result items
			for (int i = 0; i < sourceData.Length; i++)
			{
				if(sourceData[i].WorkDuration != resultData[i].WorkDuration)
				{
					Console.WriteLine("Discrepancy at index {0}", i);
					break;
				}
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
