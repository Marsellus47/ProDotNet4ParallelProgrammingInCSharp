using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing07
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			// Create the antecedent task
			Task task = new Task(() =>
			{
				// Write out a message
				Console.WriteLine("Antecedent running");

				// Wait indefinitely on the token wait handle
				tokenSource.Token.WaitHandle.WaitOne();

				// Handle the cancellation exception
				tokenSource.Token.ThrowIfCancellationRequested();
			}, tokenSource.Token);

			// Create a selective continuation
			Task neverScheduled = task.ContinueWith(antecedent =>
			{
				// Write out a message
				Console.WriteLine("This task will never be scheduled");
			}, tokenSource.Token);

			// Create a bad selective continuation
			Task badSelective = task.ContinueWith(antecedent =>
			{
				// Write out a message
				Console.WriteLine("This task will never be scheduled");
			}, tokenSource.Token, TaskContinuationOptions.OnlyOnCanceled, TaskScheduler.Current);

			// Create a good selective continuation
			Task continuation = task.ContinueWith(antecedent =>
			{
				// Write out a message
				Console.WriteLine("Continuation running");
			}, TaskContinuationOptions.OnlyOnCanceled);

			// Start the task
			task.Start();

			// Prompt the user so they can cancel the token
			Console.WriteLine("Press enter to cancel token");
			Console.ReadLine();

			// Cancel the task
			tokenSource.Cancel();

			// Wait for the good continuation to complete
			continuation.Wait();

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
