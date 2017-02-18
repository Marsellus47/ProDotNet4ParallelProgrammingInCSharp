using Listing01;
using System;
using System.Threading.Tasks;

namespace Listing03
{
	class Program
	{
		static void Main(string[] args)
		{
			Task<BankAccount> rootTask = new Task<BankAccount>(() =>
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

			// Create the second-generation task, which will double the antecedent balance
			Task<int> continuationTask1 = rootTask.ContinueWith<int>((antecedent) =>
			{
				Console.WriteLine("Interim Balance 1: {0}", antecedent.Result.Balance);
				return antecedent.Result.Balance * 2;
			});

			// Create the third-genration task, which will print out the result
			Task continuationTask2 = continuationTask1.ContinueWith((antecedent) =>
			{
				Console.WriteLine("Final Balance 1: {0}", antecedent.Result);
			});

			// Create a second and third-genration task in one step
			rootTask
				.ContinueWith<int>((antecedent) =>
				{
					Console.WriteLine("Interim Balance 2: {0}", antecedent.Result.Balance);
					return antecedent.Result.Balance / 2;
				})
				.ContinueWith((antecedent) =>
				{
					Console.WriteLine("Final Balance 2: {0}", antecedent.Result);
				});

			// Start the task
			rootTask.Start();

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
