using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing21
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the cancellation token source and the token
			CancellationTokenSource tokenSource = new CancellationTokenSource();
			CancellationToken token = tokenSource.Token;

			// Create a task that throws an exception
			Task task1 = new Task(() =>
			{
				throw new NullReferenceException();
			});

			Task task2 = new Task(() =>
			{
				// Wait until we are cancelled
				token.WaitHandle.WaitOne(-1);
				throw new OperationCanceledException();
			}, token);

			// Start the tasks
			task1.Start();
			task2.Start();

			// Cancel the token
			tokenSource.Cancel();

			// Wait for the tasks, ignoring exceptions
			try
			{
				Task.WaitAll(task1, task2);
			}
			catch (AggregateException)
			{
				// Ignore exceptions
			}

			// Write out the details of the task exception
			Console.WriteLine("Task 1 completed: {0}", task1.IsCompleted);
			Console.WriteLine("Task 1 faulted: {0}", task1.IsFaulted);
			Console.WriteLine("Task 1 cancelled: {0}", task1.IsCanceled);
			Console.WriteLine(task1.Exception);

			// Write out the details of the task exception
			Console.WriteLine("Task 2 completed: {0}", task2.IsCompleted);
			Console.WriteLine("Task 2 faulted: {0}", task2.IsFaulted);
			Console.WriteLine("Task 2 cancelled: {0}", task2.IsCanceled);
			Console.WriteLine(task2.Exception);

			// Wait for input before exiting
			Console.WriteLine("Main method complete. Press enter to finish.");
			Console.ReadLine();
		}
	}
}
