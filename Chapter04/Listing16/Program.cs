using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing16
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the primitive
			ManualResetEventSlim manualResetEvent = new ManualResetEventSlim();

			// Create the cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			// Create and start the task that will wait on the event
			Task waitingTask = Task.Factory.StartNew(() =>
			{
				while(true)
				{
					// Wait on the primitive
					manualResetEvent.Wait(tokenSource.Token);

					// Print out the message
					Console.WriteLine("Waiting task active");
				}
			}, tokenSource.Token);

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
					manualResetEvent.Set();
					Console.WriteLine("Event set");

					// Go to sleep again
					tokenSource.Token.WaitHandle.WaitOne(rnd.Next(500, 2000));

					// Reset the event
					manualResetEvent.Reset();
					Console.WriteLine("Event reset");
				}

				// If we reach this point, we know the task has been cancelled
				tokenSource.Token.ThrowIfCancellationRequested();
			}, tokenSource.Token);

			// Ask the user to press return before we cancel the token and bring the tasks to an end
			Console.WriteLine("Press enter to cancel tasks");
			Console.ReadLine();

			// Cancel the token source and wait for the tasks
			tokenSource.Cancel();
			try
			{
				Task.WaitAll(waitingTask, signallingTask);
			}
			catch(AggregateException)
			{
				// Discard exceptions
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
