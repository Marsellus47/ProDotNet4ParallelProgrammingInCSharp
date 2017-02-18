using Listing01;
using System;
using System.Threading.Tasks;

namespace Listing05
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the bank account instance
			BankAccount account = new BankAccount();

			// Create an array of tasks
			Task<int>[] tasks = new Task<int>[10];

			for (int i = 0; i < tasks.Length; i++)
			{
				// Create a new task
				tasks[i] = new Task<int>(stateObject =>
				{
					// Get the state object
					int isolatedBalance = (int)stateObject;

					// Enter a loop for 1000 balance updates
					for (int j = 0; j < 1000; j++)
					{
						// Update the balance
						isolatedBalance++;
					}

					// Return the updated balance
					return isolatedBalance;
				}, account.Balance);
			}

			// Set up a multitask continuation
			Task continuation = Task.Factory.ContinueWhenAll<int>(tasks, antecedents =>
			{
				// Run through and sum the individual balances
				foreach (var task in antecedents)
				{
					account.Balance += task.Result;
				}
			});

			// Start the antecedent tasks
			foreach (var task in tasks)
			{
				task.Start();
			}

			// Wait for the continuation task to complete
			continuation.Wait();

			// Write out the counter value
			Console.WriteLine("Expected value: {0}, Balance: {1}",
				10000, account.Balance);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
