using System;
using System.Threading.Tasks;

namespace Listing09
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
				// Check to see if the antecedent threw an exception
				if(antecedent.Status == TaskStatus.Faulted)
				{
					// Get and rethrow the antecedent exception
					throw antecedent.Exception.InnerException;
				}

				// Write out a message
				Console.WriteLine("Third generation task");
			});

			// Start the first generation task
			gen1.Start();

			try
			{
				// Wait for the last task in the chain to complete
				gen3.Wait();
			}
			catch (AggregateException ex)
			{
				ex.Handle(inner =>
				{
					Console.WriteLine("Handled exception of type: {0}", inner.GetType());
					return true;
				});
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
