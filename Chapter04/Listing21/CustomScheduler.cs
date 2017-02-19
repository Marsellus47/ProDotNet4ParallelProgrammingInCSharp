using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Listing21
{
	public class CustomScheduler : TaskScheduler, IDisposable
    {
		private readonly BlockingCollection<Task> taskQueue;
		private readonly Thread[] threads;

		public CustomScheduler(int concurrency)
		{
			// Initialize the collection and thread array
			taskQueue = new BlockingCollection<Task>();
			threads = new Thread[concurrency];

			// Create and start the threads
			for (int i = 0; i < threads.Length; i++)
			{
				(threads[i] = new Thread(() =>
				{
					// Loop while the blocking collection is not complete and try to execute the next task
					foreach (var task in taskQueue.GetConsumingEnumerable())
					{
						TryExecuteTask(task);
					}
				})).Start();
			}
		}

		protected override void QueueTask(Task task)
		{
			if(task.CreationOptions.HasFlag(TaskCreationOptions.LongRunning))
			{
				// Create a dedicated thread to execute this task
				new Thread(() =>
				{
					TryExecuteTask(task);
				}).Start();
			}
			else
			{
				// Add the task to the queue
				taskQueue.Add(task);
			}
		}

		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			// Only allow inline execution if the executing thread is one belonging to this scheduler
			if(threads.Contains(Thread.CurrentThread))
			{
				return TryExecuteTask(task);
			}
			else
			{
				return false;
			}
		}

		public override int MaximumConcurrencyLevel
		{
			get
			{
				return threads.Length;
			}
		}

		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return taskQueue.ToArray();
		}

		public void Dispose()
		{
			// Mark the collection as complete
			taskQueue.CompleteAdding();

			// Wait for each of the threads to finish
			foreach (var thread in threads)
			{
				thread.Join();
			}
		}
	}
}
