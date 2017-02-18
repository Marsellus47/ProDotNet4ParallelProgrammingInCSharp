using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Excessive_Spinning
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			// Create the first task
			Task t1 = Task.Factory.StartNew(() =>
			{
				Console.WriteLine("Task 1 waiting for cancellation");
				tokenSource.Token.WaitHandle.WaitOne();
				Console.WriteLine("Task 1 cancelled");
				tokenSource.Token.ThrowIfCancellationRequested();
			}, tokenSource.Token);

			// Create the second task, which will use a code loop
			Task t2 = Task.Factory.StartNew(() =>
			{
				// Enter a loop until t1 is cancelled
				while(!t1.Status.HasFlag(TaskStatus.Canceled))
				{
					// Do nothing - this is a code loop
				}
				Console.WriteLine("Task 2 exited code loop");
			});

			// Create the third task, which will use spin waiting
			Task t3 = Task.Factory.StartNew(() =>
			{
				// Enter the spin wait loop
				while (t1.Status != TaskStatus.Canceled)
				{
					Thread.SpinWait(1000);
				}
				Console.WriteLine("Task 3 exited spin wait loop");
			});

			// Prompt the user to hit enter to cancel
			Console.WriteLine("Press enter to cancel token");
			Console.ReadLine();
			tokenSource.Cancel();

			// Wait for input before exiting
			Console.WriteLine("Main method complete. Press enter to finish.");
			Console.ReadLine();
		}
	}
}
