using Listing08;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing09
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
					// Get a local copy of the shared data
					int startBalance = account.Balance;

					// Create a local copy of the shared data
					int localBalance = startBalance;

					// Enter a loop for 1000 balance updates
					for (int j = 0; j < 1000; j++)
					{
						// Update the local balance
						localBalance++;
					}

					// Check to see if the shared data has changed since we started
					// and if not, then update with our local value
					int sharedData = Interlocked.CompareExchange(ref account.Balance, localBalance, startBalance);

					if(sharedData == startBalance)
					{
						Console.WriteLine("Shared data update OK");
					}
					else
					{
						Console.WriteLine("Shared data changed");
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
