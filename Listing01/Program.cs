using System;
using System.Threading.Tasks;

namespace Listing01
{
	class Program
	{
		static void Main(string[] args)
		{
			Task<BankAccount> task = new Task<BankAccount>(() =>
			{
				// Create a new bank account
				BankAccount account = new BankAccount();

				// Enter a loop
				for(int i = 0; i < 1000; i++)
				{
					// Increment the account total
					account.Balance++;
				}

				// Return the bank account
				return account;
			});

			task.ContinueWith((antecedent) =>
			{
				Console.WriteLine("Final balance: {0}", antecedent.Result.Balance);
			});

			// Start the task
			task.Start();

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
