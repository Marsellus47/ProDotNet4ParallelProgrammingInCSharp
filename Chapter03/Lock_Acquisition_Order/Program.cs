using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lock_Acquisition_Order
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create two lock objects
			object lock1 = new object();
			object lock2 = new object();

			// Create a task that acquires lock 1 and then lock 2
			Task task1 = new Task(() =>
			{
				lock(lock1)
				{
					Console.WriteLine("Task 1 acquired lock 1");
					Thread.Sleep(500);
					lock(lock2)
					{
						Console.WriteLine("Task 1 acquired lock 2");
					}
				}
			});

			// Create a task that acquires lock 2 and then lock 1
			Task task2 = new Task(() =>
			{
				lock (lock2)
				{
					Console.WriteLine("Task 2 acquired lock 2");
					Thread.Sleep(500);
					lock (lock1)
					{
						Console.WriteLine("Task 2 acquired lock 1");
					}
				}
			});

			// Start the tasks
			task1.Start();
			task2.Start();

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
