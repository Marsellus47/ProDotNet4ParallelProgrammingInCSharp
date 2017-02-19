using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Reusing_Objects_in_Producers
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a blocking collection
			BlockingCollection<DataItem> blockingCollection = new BlockingCollection<DataItem>();

			// Create and start a consumer
			Task consumer = Task.Factory.StartNew(() =>
			{
				// Define a data item to use in the loop
				DataItem item;

				while (!blockingCollection.IsCompleted)
				{
					if(blockingCollection.TryTake(out item))
					{
						Console.WriteLine("Item counter {0}", item.Counter);
					}
				}
			});

			// Create and start a producer
			Task.Factory.StartNew(() =>
			{
				// Create a data item to use in the loop
				DataItem item = new DataItem();

				for (int i = 0; i < 100; i++)
				{
					// Set the numeric value
					item.Counter = i;

					// Add the item to the collection
					blockingCollection.Add(item);
				}

				// Mark the collection as finished
				blockingCollection.CompleteAdding();
			});

			// Wait for the consumer to finish
			consumer.Wait();

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
