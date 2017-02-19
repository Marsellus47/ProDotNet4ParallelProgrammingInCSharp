using System;
using System.Threading.Tasks;

namespace Listing01
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the arrays to hold the data and the results
			int[] dataItems = new int[100];
			double[] resultItems = new double[100];

			// Create the data items
			for (int i = 0; i < dataItems.Length; i++)
			{
				dataItems[i] = i;
			}

			// Process the data in a parallel for loop
			Parallel.For(0, dataItems.Length, index =>
			{
				resultItems[index] = Math.Pow(dataItems[index], 2);
			});

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
