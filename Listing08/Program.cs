using System;
using System.Threading.Tasks;

namespace Listing08
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a first generation task
			Task gen1 = new Task(() =>
			{
				// Write out a message
				Console.WriteLine("First generation task");
			});

			// Create a second generation task
			Task gen2 = gen1.ContinueWith(antecedent =>
			{
				// Write out a message
				Console.WriteLine("Second generation task - throws exception");
				throw new Exception();
			});

			// Create a third generation task
			Task gen3 = gen2.ContinueWith(antecedent =>
			{
				// Write out a message
				Console.WriteLine("Third generation task");
			});

			// Start the first generation task
			gen1.Start();

			// Wait for the last task in the chain to complete
			gen3.Wait();

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
