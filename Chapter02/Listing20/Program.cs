using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing20
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the cancellation token source and the token
			CancellationTokenSource tokenSource = new CancellationTokenSource();
			CancellationToken token = tokenSource.Token;

			// Create a task that waits on the cancellation token
			Task task1 = new Task(() =>
			{
				for (int i = 0; i < 5; i++)
				{
					// Wait forever or until the token is cancelled
					token.WaitHandle.WaitOne(-1);

					// Throw an exception to acknowledge the cancellation
					throw new OperationCanceledException(token);
				}
				Console.WriteLine("Task 1 complete");
			}, token);

			// Create a task that throws an exception
			Task task2 = new Task(() =>
			{
				throw new NullReferenceException();
			});

			// Start the tasks
			task1.Start();
			task2.Start();

			// Cancel the token
			tokenSource.Cancel();

			// Wait on the tasks and catch any exceptions
			try
			{
				Task.WaitAll(task1, task2);
			}
			catch(AggregateException ex)
			{
				// Iterate through the inner exceptions using the handle method
				ex.Handle((inner) =>
				{
					if(inner is OperationCanceledException)
					{
						// ...handle task cancellation
						return true;
					}
					else
					{
						// This is an exception we don't know how to handle, so return false
						return false;
					}
				});
			}

			// Wait for input before exiting
			Console.WriteLine("Main method complete. Press enter to finish.");
			Console.ReadLine();
		}
	}
}
