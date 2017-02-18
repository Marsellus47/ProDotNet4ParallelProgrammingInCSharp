using System;
using System.Threading.Tasks;

namespace Listing05
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the task
			Task<int> task1 = new Task<int>(() =>
			{
				int sum = 0;
				for(int i = 0; i < 100; i++)
				{
					sum += i;
				}
				return sum;
			});

			// Start the task
			task1.Start();

			// Write out the result
			Console.WriteLine("Result 1: {0}", task1.Result);

			// Create the task using state
			Task<int> task2 = new Task<int>(obj =>
			{
				int sum = 0;
				int max = (int)obj;
				for(int i = 0; i < max; i++)
				{
					sum += i;
				}
				return sum;
			}, 100);

			// Start the task
			task2.Start();

			// Write out the result
			Console.WriteLine("Result 2: {0}", task2.Result);

			// Wait for input before exiting
			Console.WriteLine("Main method complete. Press enter to finish.");
			Console.ReadLine();
		}
	}
}
