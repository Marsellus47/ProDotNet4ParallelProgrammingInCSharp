using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assuming_WaitAny_Status
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a cancellation token
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			Task<int>[] tasks = new Task<int>[2];

			tasks[0] = new Task<int>(() =>
			{
				while (true)
				{
					// Sleep
					Thread.Sleep(100000);
				}
			});

			tasks[1] = new Task<int>(() =>
			{
				// Wait for the token to be cancelled
				tokenSource.Token.WaitHandle.WaitOne();

				// Throw a cancellation exception
				tokenSource.Token.ThrowIfCancellationRequested();

				// Return a result to satisfy the compiler
				return 200;
			}, tokenSource.Token);

			Task.Factory.ContinueWhenAny(tasks, antecedent =>
			{
				Console.WriteLine("Result of antecedent is {0}", antecedent.Result);
			});

			// Start the tasks
			tasks[0].Start();
			tasks[1].Start();

			// Prompt the user and cancel the token
			Console.WriteLine("Press enter to cancel token");
			Console.ReadLine();
			tokenSource.Cancel();

			// Wait for the input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
