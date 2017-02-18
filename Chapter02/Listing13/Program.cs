using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing13
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			// Create the cancellation token
			CancellationToken token = tokenSource.Token;

			// Create the task, which we will let run freely
			Task task = new Task(() =>
			{
				for (int i = 0; i < 10; i++)
				{
					// Put the task to sleep for 10 seconds
					bool cancelled = token.WaitHandle.WaitOne(10000);

					// Print out a message
					Console.WriteLine("Task 1 - Int value {0}. Cancelled? {1}", i, cancelled);

					// Check to see if we have been cancelled
					if(cancelled)
					{
						throw new OperationCanceledException(token);
					}
				}
			}, token);

			// Start the task
			task.Start();

			// Cancel the token source
			Console.WriteLine("Press enter to cancel token.");
			Console.ReadLine();
			tokenSource.Cancel();

			// Wait for input before exiting
			Console.WriteLine("Main method complete. Press enter to finish.");
			Console.ReadLine();
		}
	}
}
