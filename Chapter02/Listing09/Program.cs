﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing09
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			// Create the cancellation token
			CancellationToken token = tokenSource.Token;

			// Create the task
			Task task1 = new Task(() =>
			{
				for (int i = 0; i < int.MaxValue; i++)
				{
					if (token.IsCancellationRequested)
					{
						Console.WriteLine("Task cancel detected");
						throw new OperationCanceledException(token);
					}
					else
					{
						Console.WriteLine("Int value {0}", i);
					}
				}
			}, token);

			// Create a second task that will use the wait handle
			Task task2 = new Task(() =>
			{
				// Wait on the handle
				token.WaitHandle.WaitOne();

				// Write out a message
				Console.WriteLine(">>>>>> Wait handle released");
			});

			// Wait for input before we start the task
			Console.WriteLine("Press enter to start task");
			Console.WriteLine("Press enter again to cancel task");
			Console.ReadLine();

			// Start the tasks
			task1.Start();
			task2.Start();

			// Read a line from the console
			Console.ReadLine();

			// Cancel the task
			Console.WriteLine("Cancelling task");
			tokenSource.Cancel();

			// Wait for input before exiting
			Console.WriteLine("Main method complete. Press enter to finish.");
			Console.ReadLine();
		}
	}
}
