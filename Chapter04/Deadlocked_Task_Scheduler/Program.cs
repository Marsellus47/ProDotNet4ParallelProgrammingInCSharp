using System;
using System.Threading;
using System.Threading.Tasks;

namespace Deadlocked_Task_Scheduler
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the scheduler
			CustomTaskScheduler scheduler = new CustomTaskScheduler(5);

			// Create a cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			Task[] tasks = new Task[6];

			for (int i = 0; i < tasks.Length; i++)
			{
				tasks[i] = Task.Factory.StartNew((object stateObject) =>
				{
					int index = (int)stateObject;
					if(index < tasks.Length -1)
					{
						Console.WriteLine("Task {0} waiting for {1}", index, index + 1);
						tasks[index + 1].Wait();
					}

					Console.WriteLine("Task {0} complete", index);
				}, i, tokenSource.Token, TaskCreationOptions.None, scheduler);
			}

			Task.WaitAll(tasks);
			Console.WriteLine("All tasks complete");

			// Wait for the input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
