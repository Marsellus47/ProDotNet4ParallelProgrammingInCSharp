using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing16
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the reader-writer lock
			ReaderWriterLockSlim rwlock = new ReaderWriterLockSlim();

			// Create a cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			// Create som shared data
			int sharedData = 0;

			// Create an array of tasks
			Task[] readerTasks = new Task[5];

			for (int i = 0; i < readerTasks.Length; i++)
			{
				// Create a new task
				readerTasks[i] = new Task(() =>
				{
					while(true)
					{
						// Acquire the read lock
						rwlock.EnterReadLock();

						// We now have the lock
						Console.WriteLine("Read lock acquired - count: {0}", rwlock.CurrentReadCount);

						// Wait - slow things down to make the example clear
						tokenSource.Token.WaitHandle.WaitOne(1000);

						// Release the read lock
						rwlock.ExitReadLock();
						Console.WriteLine("Read lock released - count: {0}", rwlock.CurrentReadCount);

						// Check for cancellation
						tokenSource.Token.ThrowIfCancellationRequested();
					}
				}, tokenSource.Token);

				// Start the new task
				readerTasks[i].Start();
			}

			Task[] writerTasks = new Task[2];
			for (int i = 0; i < writerTasks.Length; i++)
			{
				// Create a new task
				writerTasks[i] = new Task(() =>
				{
					while (true)
					{
						// Acquire the upgradeable lock
						rwlock.EnterUpgradeableReadLock();

						// Simulate a branch that will require a write
						if(true)
						{
							// Acquire the write lock
							rwlock.EnterWriteLock();

							// Print out a message with the details of the lock
							Console.WriteLine("Write lock acquired - waiting readers: {0}, writers: {1}, upgraders: {2}",
								rwlock.CurrentReadCount, rwlock.WaitingWriteCount, rwlock.WaitingUpgradeCount);

							// Modify the shared data
							sharedData++;

							// Wait - slow things down to make the example clear
							tokenSource.Token.WaitHandle.WaitOne(1000);

							// Release the write lock
							rwlock.ExitWriteLock();
						}

						// Release the upgradeable lock
						rwlock.ExitUpgradeableReadLock();

						// Check for cancellation
						tokenSource.Token.ThrowIfCancellationRequested();
					}
				}, tokenSource.Token);

				// Start the new task
				writerTasks[i].Start();
			}

			// Prompt the user
			Console.WriteLine("Press enter to acquire write lock");

			// Wait for the user to press enter
			Console.ReadLine();

			// Cancel the tasks
			tokenSource.Cancel();

			try
			{
				// Wait for the tasks to complete
				Task.WaitAll(readerTasks);
			}
			catch (AggregateException agex)
			{
				agex.Handle(ex => true);
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
