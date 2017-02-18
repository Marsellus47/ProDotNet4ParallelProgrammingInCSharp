using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Listing13
{
	class Program
	{
		static void Main(string[] args)
		{
			// Declare the name we will use for the mutex
			const string mutexName = "myAppressMutex";

			// Declare the mutex
			Mutex namedMutex;

			try
			{
				// Test to see if the named mutex already exists
				namedMutex = Mutex.OpenExisting(mutexName);
				Console.WriteLine("New Mutex acquired");
			}
			catch(WaitHandleCannotBeOpenedException)
			{
				// The mutex does not exist - we must create it
				namedMutex = new Mutex(false, mutexName);
				Console.WriteLine("Existing Mutex acquired");
			}

			// Create the task
			Task task = new Task(() =>
			{
				while(true)
				{
					// Acquire the mutex
					Console.WriteLine("Waiting to acquire Mutex");
					namedMutex.WaitOne();
					Console.WriteLine("Acquire Mutex - press enter to release");
					Console.ReadLine();
					namedMutex.ReleaseMutex();
					Console.WriteLine("Released Mutex");
				}
			});

			// Start the task
			task.Start();

			// Wait for the task to complete
			task.Wait();

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
