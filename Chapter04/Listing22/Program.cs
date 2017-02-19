using Listing21;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing22
{
	class Program
	{
		static void Main(string[] args)
		{
			// Get the processor count for the system
			int processors = Environment.ProcessorCount;

			// Create a custom scheduler
			CustomScheduler scheduler = new CustomScheduler(processors);

			Console.WriteLine("Custom scheduler ID: {0}", scheduler.Id);
			Console.WriteLine("Default scheduler ID: {0}", TaskScheduler.Default.Id);

			// Create a cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			// Create a task
			Task task = new Task(() =>
			{
				Console.WriteLine("Main Task {0} executed by scheduler {1}",
					Task.CurrentId, TaskScheduler.Current.Id);

				// Create a child task - this will use the same scheduler as its parent
				Task.Factory.StartNew(() =>
				{
					Console.WriteLine("Child Task(1) {0} executed by scheduler {1}",
						Task.CurrentId, TaskScheduler.Current.Id);
				});

				// Create a child and specify the default scheduler
				Task.Factory.StartNew(() =>
				{
					Console.WriteLine("Child Task(2) {0} executed by scheduler {1}",
						Task.CurrentId, TaskScheduler.Current.Id);
				}, tokenSource.Token, TaskCreationOptions.None, TaskScheduler.Default);
			});

			// Start the task using the custom scheduler
			task.Start(scheduler);

			// Create a continuation - this will use the default scheduler
			task.ContinueWith(antecedent =>
			{
				Console.WriteLine("Continuation Task(1) {0} executed by scheduler {1}",
					Task.CurrentId, TaskScheduler.Current.Id);
			});

			// Create a continuation using the custom scheduler
			task.ContinueWith(antecedent =>
			{
				Console.WriteLine("Continuation Task(2) {0} executed by scheduler {1}",
					Task.CurrentId, TaskScheduler.Current.Id);
			}, scheduler);
		}
	}
}
