using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Decoupled_Console
{
	public class DecoupledConsole
	{
		// Queue-based blocking collection
		private static BlockingCollection<Action> blockingQueue;

		// Task that processes messages to the console
		private static Task messageWorker;

		static DecoupledConsole()
		{
			blockingQueue = new BlockingCollection<Action>();

			// Create and start the worker task
			messageWorker = Task.Factory.StartNew(() =>
			{
				foreach (var action in blockingQueue.GetConsumingEnumerable())
				{
					action.Invoke();
				}
			}, TaskCreationOptions.LongRunning);
		}

		public static void WriteLine(object value)
		{
			blockingQueue.Add(new Action(() => Console.WriteLine(value)));
		}

		public static void WriteLine(string format, params object[] values)
		{
			blockingQueue.Add(new Action(() => Console.WriteLine(format, values)));
		}
	}
}
