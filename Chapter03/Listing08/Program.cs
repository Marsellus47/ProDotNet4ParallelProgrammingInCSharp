using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing08
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the bank account instance
			BankAccount account = new BankAccount();

			// Create an array of tasks
			Task[] tasks = new Task[10];

			// Create the lock object
			object lockObject = new object();

			for (int i = 0; i < tasks.Length; i++)
			{
				// Create the new task
				tasks[i] = new Task(() =>
				{
					// Enter a loop for 1000 balance updates
					for (int j = 0; j < 1000; j++)
					{
						// Update the balance
						Interlocked.Increment(ref account.Balance);
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
