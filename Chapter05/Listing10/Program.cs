using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing10
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			// Start a task that will cancel the token source after a few seconds sleep
			Task.Factory.StartNew(() =>
			{
				// Sleep for 5 seconds
				Thread.Sleep(5000);

				// Cancel the token source
				tokenSource.Cancel();

				// Let the user know
				Console.WriteLine("Token cancelled");
			}, tokenSource.Token);

			// Create a loop options with a token
			ParallelOptions loopOptions = new ParallelOptions { CancellationToken = tokenSource.Token };

			try
			{
				// Perform a parallel loop specifying the options make this a loop that will
				// take a while to complete so the user has time to cancel
				Parallel.For(0, Int64.MaxValue, loopOptions, index =>
				{
					// Do something just to ocupy the cpu for a little
					double result = Math.Pow(index, 3);

					// Write out the current index
					Console.WriteLine("Index {0}, result {1}",
						index, result);

					// Put the thread to sleep, just to slow things down
					Thread.Sleep(100);
				});
			}
			catch(OperationCanceledException)
			{
				Console.WriteLine("Caught cancellation exception...");
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
