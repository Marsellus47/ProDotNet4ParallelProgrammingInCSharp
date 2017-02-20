using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Listing16
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the source data
			IList<string> sourceData = new List<string> { "an", "apple", "a", "day", "keeps", "the", "doctor", "away" };

			// Create an array to hold the results
			string[] resultData = new string[sourceData.Count];

			// Create an orderable partitioner
			OrderablePartitioner<string> partitioner = Partitioner.Create(sourceData);

			// Perform the parallel loop
			Parallel.ForEach(partitioner, (string item, ParallelLoopState loopState, long index) =>
			{
				// Process the item
				if(item == "apple")
				{
					item = "apricot";
				}

				// Use the index to set the result in the array
				resultData[index] = item;
			});

			// Print out the contents of the result array
			for (int i = 0; i < resultData.Length; i++)
			{
				Console.WriteLine("Item {0} is {1}", i, resultData[i]);
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
