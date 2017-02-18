using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing10
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
				for(int i = 0; i < int.MaxValue; i++)
				{
					token.ThrowIfCancellationRequested();
					Console.WriteLine("Task 1 - Int value {0}", i);
				}
			}, token);

			Task task2 = new Task(() =>
			{
				for (int i = 0; i < int.MaxValue; i++)
				{
					token.ThrowIfCancellationRequested();
					Console.WriteLine("Task 2 - Int value {0}", i);
				}
			}, token);

			// Wait for input before we start the task
			Console.WriteLine("Press enter to start task");
			Console.WriteLine("Press enter again to cancel task");
			Console.ReadLine();

			// Start the tasks
			task1.Start();
			task2.Start();

			// Read a line from the console
			Console.ReadLine();

			// Cancel the tasks
			Console.WriteLine("Cancelling tasks");
			tokenSource.Cancel();

			// Wait for input before exiting
			Console.WriteLine("Main method complete. Press enter to finish.");
			Console.ReadLine();
		}
	}
}
