using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing22
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the new escalation policy
			TaskScheduler.UnobservedTaskException +=
				(object sender, UnobservedTaskExceptionEventArgs eventArgs) =>
			{
				// Mark the exception as being handled
				eventArgs.SetObserved();

				// Get the aggregate exception and process the contents
				((AggregateException)eventArgs.Exception).Handle(ex =>
				{
					// Write the type of the exception to the console
					Console.WriteLine("Exception type: {0}", ex.GetType());
					return true;
				});
			};

			// Create tasks that will throw an exception
			Task task1 = new Task(() =>
			{
				throw new NullReferenceException();
			});

			Task task2 = new Task(() =>
			{
				throw new ArgumentOutOfRangeException();
			});

			// Start the tasks
			task1.Start();
			task2.Start();

			// Wait for the tasks to complete - but do so without calling any of the trigger members
			// so that the exceptions remain unhandled
			while(!task1.IsCompleted || !task2.IsCompleted)
			{
				Thread.Sleep(500);
			}

			// Wait for input before exiting
			Console.WriteLine("Main method complete. Press enter to finish.");
			Console.ReadLine();
		}
	}
}
