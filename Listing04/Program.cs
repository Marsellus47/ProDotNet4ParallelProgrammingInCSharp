using System;
using System.Threading.Tasks;

namespace Listing04
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the first generation task
			Task firstGen = new Task(() =>
			{
				Console.WriteLine("Message from first generation task");
				// Comment out this line to stop the fault
				throw new Exception();
			});

			// Create the second-genration task - only to run on exception
			Task secondGenOnFault = firstGen.ContinueWith(antecedent =>
			{
				// Write out a message with the antecedent exception
				Console.WriteLine("Antecedent task faulted with type: {0}", antecedent.Exception.InnerException.GetType());
			}, TaskContinuationOptions.OnlyOnFaulted);

			// Create the second-genration task - only to run on no exception
			Task secondGenNotOnFault = firstGen.ContinueWith(antecedent =>
			{
				Console.WriteLine("Antecedent task NOT faulted");
			}, TaskContinuationOptions.NotOnFaulted);

			// Start the first generation task
			firstGen.Start();

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
