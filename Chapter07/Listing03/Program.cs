using System;
using System.Threading.Tasks;

namespace Listing03
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a lock object
			object lockObject = new object();

			// Create a sequence of tasks that acquire the lock in ordere to perform a time-expensive function over and over
			Task[] tasks = new Task[10];
			for (int i = 0; i < tasks.Length; i++)
			{
				tasks[i] = Task.Factory.StartNew(() =>
				{
					// Acquire the lock
					lock (lockObject)
					{
						// Perform some work
						for (int index = 0; index < 50000000; index++)
						{
							Math.Pow(index, 2);
						}
					}
				});
			}

			// Wait for the tasks to complete
			Task.WaitAll(tasks);
		}
	}
}
