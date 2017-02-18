using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Listing16
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			// Create the cancellation token
			CancellationToken token = tokenSource.Token;

			// Create and start the first task, which we will let run fully
			Task task = CreateTask(token);
			task.Start();

			// Wait for the task
			Console.WriteLine("Waiting for task to complete.");
			task.Wait();
			Console.WriteLine("Task Completed.");

			// Create and start another task
			task = CreateTask(token);
			task.Start();

			Console.WriteLine("Waiting 2 secs for task to complete.");
			bool completed = task.Wait(2000);
			Console.WriteLine("Wait ended - task completed: {0}", completed);

			// Create and start another task
			task = CreateTask(token);
			task.Start();

			Console.WriteLine("Waiting 2 secs for task to complete.");
			completed = task.Wait(2000, token);
			Console.WriteLine("Wait ended - task completed: {0}, task cancelled {1}", completed, task.IsCanceled);

			// Wait for input before exiting
			Console.WriteLine("Main method complete. Press enter to finish.");
			Console.ReadLine();
		}

		private static Task CreateTask(CancellationToken token)
		{
			return new Task(() =>
			{
				for (int i = 0; i < 5; i++)
				{
					// Check for task cancellation
					token.ThrowIfCancellationRequested();

					// Print out a message
					Console.WriteLine("Task - Int value {0}", i);

					// Put the task to sleep for 1 second
					token.WaitHandle.WaitOne(1000);
				}
			}, token);
		}
	}
}
