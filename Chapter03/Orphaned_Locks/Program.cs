using System;
using System.Threading;
using System.Threading.Tasks;

namespace Orphaned_Locks
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the sync primitive
			Mutex mutex = new Mutex();

			// Create a cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			// Create a task that acquires and releases the mutex
			Task task1 = new Task(() =>
			{
				while(true)
				{
					mutex.WaitOne();
					Console.WriteLine("Task 1 acquired mutex");

					// Wait for 500 ms
					tokenSource.Token.WaitHandle.WaitOne(500);

					// Exit the mutex
					mutex.ReleaseMutex();
					Console.WriteLine("Task 1 released mutex");
				}
			}, tokenSource.Token);

			// Create a task that acquires and then abandons the mutex
			Task task2 = new Task(() =>
			{
				// Wait for 2 seconds to let the other task run
				tokenSource.Token.WaitHandle.WaitOne(2000);

				// Acquire the mutex
				mutex.WaitOne();
				Console.WriteLine("Task 2 acquired mutex");

				// Abandon the mutex
				throw new Exception("Abandoning Mutex");
			}, tokenSource.Token);

			// Start the tasks
			task1.Start();
			task2.Start();

			// Put the main thread to sleep
			tokenSource.Token.WaitHandle.WaitOne(3000);

			// Wait for task 2
			try
			{
				task2.Wait();
			}
			catch(AggregateException ex)
			{
				ex.Handle((inner) =>
				{
					Console.WriteLine(inner);
					return true;
				});
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
