using System;
using System.Collections;
using System.Threading.Tasks;

namespace Listing22
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a collection
			Queue sharedQueue = Queue.Synchronized(new Queue());

			// Create tasks to process the list
			Task[] tasks = new Task[10];
			for(int i = 0; i < tasks.Length; i++)
			{
				// Create the new task
				tasks[i] = new Task(() =>
				{
					for(int j = 0; j < 100; j++)
					{
						sharedQueue.Enqueue(j);
					}
				});

				// Start the new task
				tasks[i].Start();
			}

			// Wait for the tasks to complete
			Task.WaitAll(tasks);

			// Report on the number of items enqueued
			Console.WriteLine("Items enqueued: {0}", sharedQueue.Count);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
