using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Using_BlockingCollection_as_IEnum
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a blocking collection
			BlockingCollection<int> blockingCollection = new BlockingCollection<int>();

			// Create and start a producer
			Task.Factory.StartNew(() =>
			{
				// Put the producer to sleep
				Thread.Sleep(500);

				for (int i = 0; i < 100; i++)
				{
					// Add the item to the collaction
					blockingCollection.Add(i);
				}

				// Mark the collection as finished
				blockingCollection.CompleteAdding();
			});

			// Create and start the consumer
			Task consumer = Task.Factory.StartNew(() =>
			{
				// Use a foreach loop to consume the blocking collection
				foreach (var item in blockingCollection)
				{
					Console.WriteLine("Item {0}", item);
				}
				Console.WriteLine("Collection is fully consumed");
			});

			// Wait for the consumer to finish
			consumer.Wait();

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
