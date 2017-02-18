using Listing01;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing11
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the bank account instance
			BankAccount account = new BankAccount();

			// Create the Mutex
			Mutex mutex = new Mutex();

			// Create an array of tasks
			Task[] tasks = new Task[10];

			for (int i = 0; i < tasks.Length; i++)
			{
				// Create the new task
				tasks[i] = new Task(() =>
				{
					// Enter a loop for 1000 balance updates
					for (int j = 0; j < 1000; j++)
					{
						// Acquire the mutex
						bool lockAcquired = mutex.WaitOne();
						try
						{
							// Update the balance
							account.Balance++;
						}
						finally
						{
							// Release the mutex
							if (lockAcquired)
							{
								mutex.ReleaseMutex();
							}
						}
					}
				});

				// Start the new task
				tasks[i].Start();
			}

			// Wait for all of the tasks to complete
			Task.WaitAll(tasks);

			// Write out the counter value
			Console.WriteLine("Expected value {0}", 10000);
			Console.WriteLine("Balance: {0}", account.Balance);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
