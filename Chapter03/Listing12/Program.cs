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
			// Create the bank account instances
			BankAccount account1 = new BankAccount();
			BankAccount account2 = new BankAccount();

			// Create the Mutexex
			Mutex mutex1 = new Mutex();
			Mutex mutex2 = new Mutex();
			
			// Create a new task to update the first account
			Task task1 = new Task(() =>
			{
				// Enter a loop for 1000 balance updates
				for (int j = 0; j < 1000; j++)
				{
					// Acquire the mutex
					bool lockAcquired = mutex1.WaitOne();
					try
					{
						// Update the balance
						account1.Balance++;
					}
					finally
					{
						// Release the mutex
						if (lockAcquired)
						{
							mutex1.ReleaseMutex();
						}
					}
				}
			});

			// Create a new task to update the second account
			Task task2 = new Task(() =>
			{
				// Enter a loop for 1000 balance updates
				for (int j = 0; j < 1000; j++)
				{
					// Acquire the mutex
					bool lockAcquired = mutex2.WaitOne();
					try
					{
						// Update the balance
						account2.Balance += 2;
					}
					finally
					{
						// Release the mutex
						if (lockAcquired)
						{
							mutex2.ReleaseMutex();
						}
					}
				}
			});

			// Create a new task to update the both accounts
			Task task3 = new Task(() =>
			{
				// Enter a loop for 1000 balance updates
				for (int j = 0; j < 1000; j++)
				{
					// Acquire the locks for both accounts
					bool lockAcquired = Mutex.WaitAll(new WaitHandle[] { mutex1, mutex2 });
					try
					{
						// Simulate a transfer between accounts
						account1.Balance++;
						account2.Balance--;
					}
					finally
					{
						// Release the mutex
						if (lockAcquired)
						{
							mutex1.ReleaseMutex();
							mutex2.ReleaseMutex();
						}
					}
				}
			});

			// Start the tasks
			task1.Start();
			task2.Start();
			task3.Start();

			// Wait for all of the tasks to complete
			Task.WaitAll(task1, task2, task3);

			// Write out the counter value
			Console.WriteLine("Account 1 balance: {0}, Account 2 balance: {1}",
				account1.Balance, account2.Balance);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
