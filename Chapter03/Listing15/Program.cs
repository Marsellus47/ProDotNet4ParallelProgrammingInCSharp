using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing15
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the reader-writer lock
			ReaderWriterLockSlim rwlock = new ReaderWriterLockSlim();

			// Create a cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			// Create an array of tasks
			Task[] tasks = new Task[5];

			for(int i = 0; i < tasks.Length; i++)
			{
				// Create a new task
				tasks[i] = new Task(() =>
				{
					while(true)
					{
						// Acquire the read lock
						rwlock.EnterReadLock();

						// We now have the lock
						Console.WriteLine("Read lock acquired - count: {0}", rwlock.CurrentReadCount);

						// Wait - this simulates a read operation
						tokenSource.Token.WaitHandle.WaitOne(1000);

						// Release the read lock
						rwlock.ExitReadLock();
						Console.WriteLine("Read lock released - count: {0}", rwlock.CurrentReadCount);

						// Check for cancellation
						tokenSource.Token.ThrowIfCancellationRequested();
					}
				}, tokenSource.Token);

				// Start the new task
				tasks[i].Start();
			}

			// Prompt the user
			Console.WriteLine("Press enter to acquire write lock");

			// Wait for the user to press enter
			Console.ReadLine();

			// Acquire the write lock
			Console.WriteLine("Requesting write lock");
			rwlock.EnterWriteLock();

			Console.WriteLine("Write lock acquired");
			Console.WriteLine("Press enter to release write lock");

			// Wait for the user to press enter
			Console.ReadLine();

			// Release the write lock
			rwlock.ExitWriteLock();

			// Wait for 2 seconds and then cancel the tasks
			tokenSource.Token.WaitHandle.WaitOne(2000);
			tokenSource.Cancel();

			try
			{
				// Wait for the tasks to complete
				Task.WaitAll(tasks);
			}
			catch(AggregateException)
			{
				// Do nothing
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
