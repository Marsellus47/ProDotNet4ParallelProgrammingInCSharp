using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing06
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create an array of tasks
			Task<int>[] tasks = new Task<int>[10];

			// Create a cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			// Create a random number generator
			Random rnd = new Random();

			for (int i = 0; i < tasks.Length; i++)
			{
				// Create a new task
				tasks[i] = new Task<int>(stateObject =>
				{
					// Define the variable for the sleep interval
					int sleepInterval;

					// Acquire exclusive access to the random number generator and get a random value
					lock(rnd)
					{
						sleepInterval = rnd.Next(500, 2000);
					}

					// Put the task thread to sleep for the interval
					tokenSource.Token.WaitHandle.WaitOne(sleepInterval);

					// Check to see the current task has been cancelled
					tokenSource.Token.ThrowIfCancellationRequested();

					// Return the sleep interval
					return sleepInterval;
				}, tokenSource.Token);
			}

			// Set up a when-any multitask continuation
			Task continuation = Task.Factory.ContinueWhenAny<int>(tasks, antecedent =>
			{
				// Write out a message using the antecedent result
				Console.WriteLine("The first task slept for {0} milliseconds", antecedent.Result);
			});

			// Start the antecedent tasks
			foreach (var task in tasks)
			{
				task.Start();
			}

			// Wait for the continuation task to complete
			continuation.Wait();

			// Cancel the remaining tasks
			tokenSource.Cancel();

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
