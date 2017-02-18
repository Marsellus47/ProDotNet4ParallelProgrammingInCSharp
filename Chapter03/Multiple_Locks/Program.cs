using Listing01;
using System;
using System.Threading.Tasks;

namespace Multiple_Locks
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the bank account instance
			BankAccount account = new BankAccount();

			// Create two lock objects
			object lock1 = new object();
			object lock2 = new object();

			// Create an array of tasks
			Task[] tasks = new Task[10];

			// Create five tasks that use the first lock object
			for(int i = 0; i < 5; i++)
			{
				// Create a new task
				tasks[i] = new Task(() =>
				{
					// Enter a loop for 1000 balance updates
					for(int j = 0; j < 1000; j++)
					{
						lock(lock1)
						{
							// Update the balance
							account.Balance++;
						}
					}
				});
			}

			// Create five tasks that use the second lock object
			for (int i = 5; i < 10; i++)
			{
				// Create a new task
				tasks[i] = new Task(() =>
				{
					// Enter a loop for 1000 balance updates
					for (int j = 0; j < 1000; j++)
					{
						lock (lock2)
						{
							// Update the balance
							account.Balance++;
						}
					}
				});
			}

			// Start the tasks
			foreach(var task in tasks)
			{
				task.Start();
			}

			// Wait for all of the tasks to complete
			Task.WaitAll(tasks);

			// Write out the counter value
			Console.WriteLine("Expected value: {0}, Balance: {1}",
				10000, account.Balance);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
