using System;

namespace Listing02
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a bank account with the default balance
			ImmutableBankAccount bankAccount1 = new ImmutableBankAccount();
			Console.WriteLine("Account Number: {0}, Account Balance: {1}",
				ImmutableBankAccount.AccountNumber, bankAccount1.Balance);

			// Create a bank account with a starting balance
			ImmutableBankAccount bankAccount2 = new ImmutableBankAccount(200);
			Console.WriteLine("Account Number: {0}, Account Balance: {1}",
				ImmutableBankAccount.AccountNumber, bankAccount2.Balance);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
