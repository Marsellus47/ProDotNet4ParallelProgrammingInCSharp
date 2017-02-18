using System;
using System.Threading.Tasks;

namespace Listing19
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the tasks
			Task task1 = new Task(() =>
			{
				ArgumentOutOfRangeException exception = new ArgumentOutOfRangeException();
				exception.Source = "task1";
				throw exception;
			});

			Task task2 = new Task(() =>
			{
				throw new NullReferenceException();
			});

			Task task3 = new Task(() => Console.WriteLine("Hello from Task 3"));

			// Start the tasks
			task1.Start();
			task2.Start();
			task3.Start();

			// Wait for all of the tasks to complete
			// and wrap the method in a try...catch block
			try
			{
				Task.WaitAll(task1, task2, task3);
			}
			catch(AggregateException ex)
			{
				// Enumerate the exceptions that have been aggregated
				foreach(var inner in ex.InnerExceptions)
				{
					Console.WriteLine("Exception type {0} from {1}.\nMessage: {2}",
						inner.GetType(), inner.Source, inner.Message);
				}
			}

			// Wait for input before exiting
			Console.WriteLine("Main method complete. Press enter to finish.");
			Console.ReadLine();
		}
	}
}
