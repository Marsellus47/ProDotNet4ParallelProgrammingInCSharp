using System;
using System.Threading.Tasks;

namespace Dependency_Deadlock
{
	class Program
	{
		static void Main(string[] args)
		{
			// Define an array to hold the Tasks
			Task<int>[] tasks = new Task<int>[2];

			// Create and start the first task
			tasks[0] = Task.Factory.StartNew(() =>
			{
				// Get the result of the other task,
				// add 100 to it and return it as the result
				return tasks[1].Result + 100;
			});

			// Create and start the first task
			tasks[1] = Task.Factory.StartNew(() =>
			{
				// Get the result of the other task,
				// add 100 to it and return it as the result
				return tasks[0].Result + 100;
			});

			// Wait for the tasks to complete
			Task.WaitAll(tasks);

			// Wait for input before exiting
			Console.WriteLine("Main method complete. Press enter to finish.");
			Console.ReadLine();
		}
	}
}
