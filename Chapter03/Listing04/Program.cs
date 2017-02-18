﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Listing01;

namespace Listing04
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the bank account instance
			BankAccount account = new BankAccount();

			// Create an array of tasks
			Task<int>[] tasks = new Task<int>[10];

			// Create the thread local storage
			ThreadLocal<int> tls = new ThreadLocal<int>();

			for (int i = 0; i < 10; i++)
			{
				// Create the new task
				tasks[i] = new Task<int>((stateObject) =>
				{
					// Get the state object and use it to set the TLS data
					tls.Value = (int)stateObject;

					// Enter a loop for 1000 balance updates
					for (int j = 0; j < 1000; j++)
					{
						// Update the balance
						tls.Value++;
					}

					// Return the updated balance
					return tls.Value;
				}, account.Balance);

				// Start the new task
				tasks[i].Start();
			}

			// Get the result from each task and add it to the balance
			for (int i = 0; i < 10; i++)
			{
				account.Balance += tasks[i].Result;
			}

			// Write out the counter value
			Console.WriteLine("Expected value {0}, Counter value: {1}", 10000, account.Balance);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
