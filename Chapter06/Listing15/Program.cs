using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Listing15
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			// Create some source data
			int[] sourceData = new int[1000000];
			for (int i = 0; i < sourceData.Length; i++)
			{
				sourceData[i] = i;
			}

			// Define a query that supports cancellation
			IEnumerable<double> results = sourceData
				.AsParallel()
				.WithCancellation(tokenSource.Token)
				.Select(item =>
				{
					// Return the result value
					return Math.Pow(item, 2);
				});

			// Create a task that will wait for 5 seconds and then cancel the token
			Task.Factory.StartNew(() =>
			{
				Thread.Sleep(5000);
				tokenSource.Cancel();
				Console.WriteLine("Token source cancelled");
			});

			try
			{
				// Enumerate the query results
				foreach (var item in results)
				{
					Console.WriteLine("Result: {0}", item);
				}
			}
			catch (OperationCanceledException)
			{
				Console.WriteLine("Caught cancellation exception");
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
