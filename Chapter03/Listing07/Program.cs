using System;
using System.Threading.Tasks;
using Listing01;

namespace Listing07
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the bank account instance
			BankAccount account = new BankAccount();

			// Create an array of tasks
			Task[] incrementTasks = new Task[5];
			Task[] decrementTasks = new Task[5];

			// Create the lock object
			object lockObject = new object();

			for (int i = 0; i < 5; i++)
			{
				// Create the new task
				incrementTasks[i] = new Task(() =>
				{
					// Enter a loop for 1000 balance updates
					for (int j = 0; j < 1000; j++)
					{
						lock (lockObject)
						{
							// Update the balance
							account.Balance++;
						}
					}
				});

				// Start the new task
				incrementTasks[i].Start();
			}

			for (int i = 0; i < 5; i++)
			{
				// Create the new task
				decrementTasks[i] = new Task(() =>
				{
					// Enter a loop for 1000 balance updates
					for (int j = 0; j < 1000; j++)
					{
						lock (lockObject)
						{
							// Update the balance
							account.Balance -= 2;
						}
					}
				});

				// Start the new task
				decrementTasks[i].Start();
			}

			// Wait for all of the tasks to complete
			Task.WaitAll(incrementTasks);
			Task.WaitAll(decrementTasks);

			// Write out the counter value
			Console.WriteLine("Expected value {0}", -5000);
			Console.WriteLine("Balance: {0}", account.Balance);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
