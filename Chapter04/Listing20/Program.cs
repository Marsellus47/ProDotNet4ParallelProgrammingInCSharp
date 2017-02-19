using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Listing20
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a pair of blocking collections that will be used to pass strings
			BlockingCollection<string> bc1 = new BlockingCollection<string>();
			BlockingCollection<string> bc2 = new BlockingCollection<string>();

			// Create another blocking collection that will be used to pass ints
			BlockingCollection<string> bc3 = new BlockingCollection<string>();

			// Craete two arrays of the blocking collections
			BlockingCollection<string>[] bc1And2 = { bc1, bc2 };
			BlockingCollection<string>[] bcAll = { bc1, bc2, bc3 };

			// Create a cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			// Create the first set of producers
			for (int i = 0; i < 5; i++)
			{
				Task.Factory.StartNew(() =>
				{
					while (!tokenSource.IsCancellationRequested)
					{
						// Compose the message
						string message = string.Format("Message from task {0}", Task.CurrentId);

						// Add the message to either collection
						BlockingCollection<string>.AddToAny(bc1And2, message, tokenSource.Token);

						// Put the task to sleep
						tokenSource.Token.WaitHandle.WaitOne(1000);
					}
				}, tokenSource.Token);
			}

			// Create the second set of producers
			for (int i = 0; i < 3; i++)
			{
				Task.Factory.StartNew(() =>
				{
					while (!tokenSource.IsCancellationRequested)
					{
						// Compose the message
						string warning = string.Format("Warning from task {0}", Task.CurrentId);

						// Add the message to collection
						bc3.Add(warning, tokenSource.Token);

						// Put the task to sleep
						tokenSource.Token.WaitHandle.WaitOne(1000);
					}
				}, tokenSource.Token);
			}

			// Create the consumers
			for (int i = 0; i < 2; i++)
			{
				Task consumer = Task.Factory.StartNew(() =>
				{
					string item;
					while (!tokenSource.IsCancellationRequested)
					{
						// Take an item from any collection
						int bcid = BlockingCollection<string>.TakeFromAny(bcAll, out item, tokenSource.Token);

						// Write out the item to the console
						Console.WriteLine("From collection {0}: {1}",
							bcid, item);
					}
				}, tokenSource.Token);
			}

			// Prompt the user to press enter
			Console.WriteLine("Press enter to cancel tasks");
			Console.ReadLine();

			// Cancel the token
			tokenSource.Cancel();

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
