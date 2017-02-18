using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Listing17
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a shared collection
			Queue<int> sharedQueue = new Queue<int>();

			// Populate the collection with items to process
			for(int i = 0; i < 1000; i++)
			{
				sharedQueue.Enqueue(i);
			}

			// Define a counter for the number of processed items
			int itemCount = 0;

			// Create tasks to process the list
			Task[] tasks = new Task[10];
			for(int i = 0; i < tasks.Length; i++)
			{
				// Create the new task
				tasks[i] = new Task(() =>
				{
					while(sharedQueue.Count > 0)
					{
						// Take an item from the queue
						int item = sharedQueue.Dequeue();

						// Increment the count of items processed
						Interlocked.Increment(ref itemCount);
					}
				});

				// Start the new task
				tasks[i].Start();
			}

			// Wait for the tasks to complete
			Task.WaitAll(tasks);

			// Report on the number of items processed
			Console.WriteLine("Items processed: {0}", itemCount);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
