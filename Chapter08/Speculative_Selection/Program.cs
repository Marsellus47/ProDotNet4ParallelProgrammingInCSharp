using System;
using System.Threading;

namespace Speculative_Selection
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create some sample functions
			Func<int, double> pFunction = value =>
			{
				Random random = new Random();
				Thread.Sleep(random.Next(1, 2000));
				return Math.Pow(value, 2);
			};

			// Create some sample functions
			Func<int, double> pFunction2 = value =>
			{
				Random random = new Random();
				Thread.Sleep(random.Next(1, 1000));
				return Math.Pow(value, 2);
			};

			// Define the callback
			Action<long, double> callback = (index, result) =>
			{
				Console.WriteLine("Received result of {0} from function {1}",
					result, index);
			};

			// Speculative compute for some values
			for (int i = 0; i < 10; i++)
			{
				SpeculativeSelection.Compute(i, callback, pFunction, pFunction2);
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
