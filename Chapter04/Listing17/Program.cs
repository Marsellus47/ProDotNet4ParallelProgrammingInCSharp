using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing17
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the primitive
			AutoResetEvent arEvent = new AutoResetEvent(false);

			// Create the cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			// Create and start the tasks that will wait on the event
			for (int i = 0; i < 3; i++)
			{
				Task.Factory.StartNew(() =>
				{
					while(!tokenSource.Token.IsCancellationRequested)
					{
						// Wait on the primitive
						arEvent.WaitOne();

						// Print out the message when we are released
						Console.WriteLine("Task {0} released", Task.CurrentId);
					}

					// If we reach this point, we know the task has been cancelled
					tokenSource.Token.ThrowIfCancellationRequested();
				}, tokenSource.Token);
			}

			// Create and start the signalling task
			Task signallingTask = Task.Factory.StartNew(() =>
			{
				// Create a random generator for sleep periods
				Random rnd = new Random();

				// Loop while the task has been not cancelled
				while (!tokenSource.Token.IsCancellationRequested)
				{
					// Go to sleep for a random period
					tokenSource.Token.WaitHandle.WaitOne(rnd.Next(500, 2000));

					// Set the event
					arEvent.Set();
					Console.WriteLine("Event set");
				}

				// If we reach this point, we know the task has been cancelled
				tokenSource.Token.ThrowIfCancellationRequested();
			}, tokenSource.Token);

			// Ask the user to press return before we cancel the token and bring the tasks to an end
			Console.WriteLine("Press enter to cancel tasks");
			Console.ReadLine();

			// Cancel the token source and wait for the tasks
			tokenSource.Cancel();

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
