using Listing01;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing12
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the array of bank accounts
			BankAccount[] accounts = new BankAccount[5];
			for (int i = 0; i < accounts.Length; i++)
			{
				accounts[i] = new BankAccount();
			}

			// Create the total balance counter
			int totalBalance = 0;

			// Create the barrier
			Barrier barrier = new Barrier(accounts.Length, myBarier =>
			{
				// Zero the balance
				totalBalance = 0;

				// Sum the account totals
				foreach (var account in accounts)
				{
					totalBalance += account.Balance;
				}

				// Write out the balance
				Console.WriteLine("Total balance: {0}", totalBalance);
			});

			// Define the tasks array
			Task[] tasks = new Task[5];

			// Loop to create the tasks
			for (int i = 0; i < tasks.Length; i++)
			{
				tasks[i] = new Task(stateObject =>
				{
					// Create a typed reference to the account
					BankAccount account = (BankAccount)stateObject;

					// Start of phase
					Random rnd = new Random();
					for (int j = 0; j < 1000; j++)
					{
						account.Balance += rnd.Next(1, 100);
					}
					// End of phase

					// Tell the user that this task has completed the phase
					Console.WriteLine("Task {0}, phase {1} ended",
						Task.CurrentId, barrier.CurrentPhaseNumber);

					// Signal the barrier
					barrier.SignalAndWait();

					// Start of phase
					// Alter the balance of this Task's account using the total balance
					// Deduct 10% of the difference from the total balance
					account.Balance -= (totalBalance - account.Balance) / 10;
					// End of phase

					// Tell the user that this task has completed the phase
					Console.WriteLine("Task {0}, phase {1} ended",
						Task.CurrentId, barrier.CurrentPhaseNumber);

					// Signal the barrier
					barrier.SignalAndWait();
				}, accounts[i]);
			}

			// Start the tasks
			foreach (var task in tasks)
			{
				task.Start();
			}

			// Wait for all of the tasks to complete
			Task.WaitAll(tasks);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
