using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing15
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a CountDownEvent with a condition counter of 5
			CountdownEvent cdEvent = new CountdownEvent(5);

			// Create a Random that we will use to generate sleep intervals
			Random rnd = new Random();

			// Create 5 tasks, each of which will wait for a random period and then signal the event
			Task[] tasks = new Task[6];
			for (int i = 0; i < tasks.Length - 1; i++)
			{
				// Create the new task
				tasks[i] = new Task(() =>
				{
					// Put the task to sleep for a random period up to one second
					Thread.Sleep(rnd.Next(500, 1000));

					// Signal the event
					Console.WriteLine("Task {0} signalling event", Task.CurrentId);
					cdEvent.Signal();
				});
			}

			// Create the final task, which will rendezvous with other 5 using the count down event
			tasks[5] = new Task(() =>
			{
				// Wait on the event
				Console.WriteLine("Rendezvous task waiting");
				cdEvent.Wait();
				Console.WriteLine("Event has been set");
			});

			// Start the tasks
			foreach (var task in tasks)
			{
				task.Start();
			}

			Task.WaitAll(tasks);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
