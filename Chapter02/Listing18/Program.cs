using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing18
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			// Create the cancellation token
			CancellationToken token = tokenSource.Token;

			// Create the tasks
			Task task1 = new Task(() =>
			{
				for (int i = 0; i < 5; i++)
				{
					// Check for task cancellation
					token.ThrowIfCancellationRequested();

					// Print out a message
					Console.WriteLine("Task 1 - Int value {0}.", i);

					// Put the task to sleep for 1 second
					Thread.SpinWait(1000);
				}
				Console.WriteLine("Task 1 complete");
			}, token);

			Task task2 = new Task(() => Console.WriteLine("Task 2 complete"), token);

			// Start the tasks
			task1.Start();
			task2.Start();

			// Wait for the tasks
			Console.WriteLine("Waiting for tasks to complete");
			int taskIndex = Task.WaitAny(task1, task2);
			Console.WriteLine("Tasks Comleted. Index: {0}", taskIndex);

			// Wait for input before exiting
			Console.WriteLine("Main method complete. Press enter to finish.");
			Console.ReadLine();
		}
	}
}
