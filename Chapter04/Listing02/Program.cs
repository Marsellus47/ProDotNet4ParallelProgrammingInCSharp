using Listing01;
using System;
using System.Threading.Tasks;

namespace Listing02
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
				for (int i = 0; i < 1000; i++)
				{
					// Increment the account total
					account.Balance++;
				}

				// Return the bank account
				return account;
			});

			Task<int> continuationTask = task.ContinueWith<int>((antecedent) =>
			{
				Console.WriteLine("Interim balance: {0}", antecedent.Result.Balance);
				return antecedent.Result.Balance * 2;
			});

			// Start the task
			task.Start();
			//continuationTask.Start(); -- Cannot start continuation task
			Console.WriteLine("Final balance: {0}", continuationTask.Result);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
