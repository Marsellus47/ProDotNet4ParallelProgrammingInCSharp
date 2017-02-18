using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing15
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
				for (int i = 0; i < Int32.MaxValue; i++)
				{
					// Put the task to sleep for 2 seconds
					Thread.SpinWait(2000);

					// Print out a message
					Console.WriteLine("Task 1 - Int value {0}.", i);

					// Check for task cancellation
					token.ThrowIfCancellationRequested();
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
