using Listing01;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Listing21
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the bank account instance
			BankAccount account = new BankAccount();

			// Create a shared dictionary
			ConcurrentDictionary<object, int> sharedDict = new ConcurrentDictionary<object, int>();

			// Create tasks to process the list
			Task<int>[] tasks = new Task<int>[10];
			for (int i = 0; i < tasks.Length; i++)
			{
				// Put the initial value into the dictionary
				sharedDict.TryAdd(i, account.Balance);

				// Create the new task
				tasks[i] = new Task<int>((keyObj) =>
				{
					// Define variables for use in the loop
					int currentValue;
					bool gotValue;

					// Enter a loop for 1000 balance updates
					for(int j = 0; j < 1000; j++)
					{
						// Get the current value from the dictionary
						gotValue = sharedDict.TryGetValue(keyObj, out currentValue);

						// Increment the value and update the dictionary
						sharedDict.TryUpdate(keyObj, currentValue + 1, currentValue);
					}

					// Define the final result
					int result;

					// Get our result from the dictionary
					gotValue = sharedDict.TryGetValue(keyObj, out result);

					// Return the result value if we got one
					if(gotValue)
					{
						return result;
					}
					else
					{
						// There was no result available - we have encountered a problem
						throw new Exception(string.Format("No data available for key {0}", keyObj));
					}
				}, i);

				// Start the new task
				tasks[i].Start();
			}

			// Update the balance of the account using the task results
			for(int i = 0; i < tasks.Length; i++)
			{
				account.Balance += tasks[i].Result;
			}

			// Write out the counter value
			Console.WriteLine("Expected value: {0}, Balance: {1}",
				10000, account.Balance);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
