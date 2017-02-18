using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing11
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the parent task
			Task parentTask = new Task(() =>
			{
				Console.WriteLine("Parent task running");
				// Create the first child task
				Task childTask = new Task(() =>
				{
					// Write out a message and wait
					Console.WriteLine("Child 1 running");
					Thread.Sleep(1000);
					Console.WriteLine("Child 1 finished");
					throw new Exception();
				}, TaskCreationOptions.AttachedToParent);

				// Create an attached continuation
				childTask.ContinueWith(antecedent =>
				{
					// Write out a message and wait
					Console.WriteLine("Continuation running");
					Thread.Sleep(1000);
					Console.WriteLine("Continuation finished");
				}, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

				Console.WriteLine("Starting child task...");
				childTask.Start();
			});

			// Start the parent task
			Console.WriteLine("Starting parent task...");
			parentTask.Start();

			try
			{
				// Wait for the parent task
				Console.WriteLine("Waiting for parent task");
				parentTask.Wait();
			}
			catch (AggregateException ex)
			{
				Console.WriteLine("Exception: {0}", ex.InnerException.GetType());
			}
			finally
			{
				Console.WriteLine("Parent task finished");
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
