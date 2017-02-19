using Listing01;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Listing19
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the blocking collection
			BlockingCollection<Deposit> blockingCollection = new BlockingCollection<Deposit>();

			// Create and start the producers, which will generate deposits and place them into the collection
			Task[] producers = new Task[3];
			for (int i = 0; i < producers.Length; i++)
			{
				producers[i] = Task.Factory.StartNew(() =>
				{
					// Create a series of deposits
					for (int j = 0; j < 20; j++)
					{
						// Create the transfer
						Deposit deposit = new Deposit { Amount = 100 };

						// Place the transfer in the collection
						blockingCollection.Add(deposit);
					}
				});
			}

			// Create a many to one continuation that will signal the end of production to the consumer
			Task.Factory.ContinueWhenAll(producers, antecedents =>
			{
				// Signal that the production has ended
				Console.WriteLine("Signalling production end");
				blockingCollection.CompleteAdding();
			});

			// Create a bank account
			BankAccount account = new BankAccount();

			// Create the consumer, which will update the balance based on the deposits
			Task consumer = Task.Factory.StartNew(() =>
			{
				while (!blockingCollection.IsCompleted)
				{
					Deposit deposit;

					// Try to take the next item
					if(blockingCollection.TryTake(out deposit))
					{
						// Update the balance with the transfer amount
						account.Balance += deposit.Amount;
					}
				}

				// Print out the final balance
				Console.WriteLine("Final Balance: {0}", account.Balance);
			});

			// Wait for the consumer to finish
			consumer.Wait();

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
