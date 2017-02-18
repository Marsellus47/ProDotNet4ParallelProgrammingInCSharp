using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing10
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
					Console.WriteLine("Child task running");
					Thread.Sleep(1000);
					Console.WriteLine("Child task finished");
					throw new Exception();
				});

				Console.WriteLine("Starting child task...");
				childTask.Start();
			});

			// Start the parent task
			Console.WriteLine("Starting parent task...");
			parentTask.Start();

			// Wait for the parent task
			Console.WriteLine("Waiting for parent task");
			parentTask.Wait();
			Console.WriteLine("Parent task finished");

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
