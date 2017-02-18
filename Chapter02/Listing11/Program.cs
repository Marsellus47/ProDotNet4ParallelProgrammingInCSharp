using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing11
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the cancellation token sources
			CancellationTokenSource tokenSource1 = new CancellationTokenSource();
			CancellationTokenSource tokenSource2 = new CancellationTokenSource();
			CancellationTokenSource tokenSource3 = new CancellationTokenSource();

			// Create a composite token source using multiple tokens
			CancellationTokenSource compositeSource = CancellationTokenSource.CreateLinkedTokenSource(tokenSource1.Token,
				tokenSource2.Token,
				tokenSource3.Token);

			// Create a cancellable task using the composite token
			Task task = new Task(() =>
			{
				// Wait until the token has been cancelled
				compositeSource.Token.WaitHandle.WaitOne();
				// Throw a cancellation exception
				throw new OperationCanceledException(compositeSource.Token);
			}, compositeSource.Token);

			// Start the task
			task.Start();

			// Cancel one of the original tokens
			tokenSource2.Cancel();

			// Wait for input before exiting
			Console.WriteLine("Main method complete. Press enter to finish.");
			Console.ReadLine();
		}
	}
}
