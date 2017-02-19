using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inconsistent_Cancellation
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			// Create the antecedent task
			Task<int> task1 = new Task<int>(() =>
			{
				// Wait for the token to be cancelled
				tokenSource.Token.WaitHandle.WaitOne();

				// Throw the cancellation exception
				tokenSource.Token.ThrowIfCancellationRequested();

				// Return the result - this code will never be reached but is required to staisfy the compiler
				return 100;
			}, tokenSource.Token);

			// Create a continuation
			// *** BAD CODE ***
			Task task2 = task1.ContinueWith(antecedent =>
			{
				// Read the antecedent result without checking the status of the task
				Console.WriteLine("Antecedent result: {0}", antecedent.Result);
			});

			// Create a continuation, but use a token
			Task task3 = task1.ContinueWith(antecedent =>
			{
				// This task will never be executed
				Console.WriteLine("Antecedent result: {0}", antecedent.Result);
			}, tokenSource.Token);

			// Create a continuation that checks the status of the antecedent
			Task task4 = task1.ContinueWith(antecedent =>
			{
				if(antecedent.Status == TaskStatus.Canceled)
				{
					Console.WriteLine("Antecedent cancelled");
				}
				else
				{
					Console.WriteLine("Antecedent result: {0}", antecedent.Result);
				}
			});

			// Prompt the user and cancel the token
			Console.WriteLine("Press enter to cancel token");
			Console.ReadLine();
			tokenSource.Cancel();

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
