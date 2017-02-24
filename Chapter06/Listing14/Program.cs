using System;
using System.Collections.Generic;
using System.Linq;

namespace Listing14
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create some source data
			int[] sourceData = new int[100];
			for (int i = 0; i < sourceData.Length; i++)
			{
				sourceData[i] = i;
			}

			// Define the query and force parallelism
			IEnumerable<double> results = sourceData.AsParallel()
				.Select(item =>
				{
					if (item == 45)
					{
						throw new Exception();
					}
					return Math.Pow(item, 2);
				});

			// Enumerate the result
			try
			{
				foreach (var item in results)
				{
					Console.WriteLine("Result {0}", item);
				}
			}
			catch(AggregateException ex)
			{
				ex.Handle(exception =>
				{
					Console.WriteLine("Handled exception of type: {0}",
						ex.InnerException.GetType());
					return true;
				});
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
