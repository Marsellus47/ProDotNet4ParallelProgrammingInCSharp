using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Trying_To_Take_Concurrently
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
				// Put items into the collection
				for (int i = 0; i < 1000; i++)
				{
					blockingCollection.Add(i);
				}
				
				// Mark the collection as complete
				blockingCollection.CompleteAdding();
			});

			// Create and start a consumer
			Task.Factory.StartNew(() =>
			{
				while (!blockingCollection.IsCompleted)
				{
					// Take an item from the collection
					int item = blockingCollection.Take();

					// Print out the item
					Console.WriteLine("Item {0}", item);
				}
			});

			// Wait for the input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
