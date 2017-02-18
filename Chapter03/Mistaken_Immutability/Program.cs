using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mistaken_Immutability
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a new instance of the immutable type
			MyImmutableType immutable = new MyImmutableType();

			// Create a cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			// Create a task that will calculate the circumference of a 1 unit circle and check the result
			Task task1 = new Task(() =>
			{
				while(true)
				{
					// Perform the calcullation
					double circ = 2 * immutable.refData.PI * immutable.circleSize;
					Console.WriteLine("Circumference: {0}", circ);

					// Check the mutation
					if(circ == 4)
					{
						// The mutation has occured - break out of the loop
						Console.WriteLine("Mutation detected");
						break;
					}

					// Sleep for a moment
					tokenSource.Token.WaitHandle.WaitOne(250);
				}
			}, tokenSource.Token);

			// Start the task
			task1.Start();

			// Wait to let the task start work
			Thread.Sleep(1000);

			immutable.refData.PI = 2;

			// Join the task
			task1.Wait();

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
