using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing13
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the barrier
			Barrier barrier = new Barrier(2);

			// Create a task that will complete
			Task.Factory.StartNew(() =>
			{
				Console.WriteLine("Good task starting phase 0");
				barrier.SignalAndWait();
				Console.WriteLine("Good task starting phase 1");
				barrier.SignalAndWait();
				Console.WriteLine("Good task completed");
			});

			// Create a task that will throw an exception
			// with a selective continuation that will reduce the
			// participant count in barrier
			Task.Factory.StartNew(() =>
			{
				Console.WriteLine("Bad task 1 throwing exception");
				throw new Exception();
			}).ContinueWith(antecedent =>
			{
				// Reduce the participant count
				Console.WriteLine("Reducing the barrier participant count");
				barrier.RemoveParticipant();
			}, TaskContinuationOptions.OnlyOnFaulted);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
