using System;

namespace Pipeline
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a set of functions that we want to pipeline together
			Func<int, double> func1 = input => Math.Pow(input, 2);
			Func<double, double> func2 = input => input / 2;
			Func<double, bool> func3 = input => input % 2 == 0 && input < 100;

			// Define a callback
			Action<int, bool> callback = (input, output) =>
			{
				if (output)
				{
					Console.WriteLine("Found value {0} with result {1}", input, output);
				}
			};

			// Create the pipeline
			Pipeline<int, bool> pipe = new Pipeline<int, double>(func1)
				.AddFunction(func2)
				.AddFunction(func3);

			// Start the pipeline
			pipe.StartProcessing();

			// Generate values and push them into the pipeline
			for (int i = 0; i < 100; i++)
			{
				Console.WriteLine("Added value {0}", i);
				pipe.AddValue(i, callback);
			}

			// Stop the pipeline
			pipe.StopProcessing();

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
