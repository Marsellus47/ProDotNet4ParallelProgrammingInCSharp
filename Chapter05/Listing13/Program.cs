using System;
using System.Threading.Tasks;

namespace Listing13
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a random number generator
			Random rnd = new Random();

			// Set the number of items created
			const int itemsPerMonth = 100000;

			// Create the source data
			Transaction[] sourceData = new Transaction[12 * itemsPerMonth];
			for (int i = 0; i < sourceData.Length; i++)
			{
				sourceData[i] = new Transaction { Amount = rnd.Next(-400, 500) };
			}

			// Create the results array
			int[] monthlyBalances = new int[12];

			for (int currentMonth = 0; currentMonth < monthlyBalances.Length; currentMonth++)
			{
				// Perform the parallel loop on the current month's data
				Parallel.For(
					currentMonth * itemsPerMonth,
					(currentMonth + 1) * itemsPerMonth,
					new ParallelOptions(),
					() => 0,
					(index, loopState, tlsBalance) =>
					{
						return tlsBalance += sourceData[index].Amount;
					},
					tlsBalance => monthlyBalances[currentMonth] += tlsBalance);

				// End of parallel for, add the previous month's balance
				if(currentMonth > 0)
				{
					monthlyBalances[currentMonth] += monthlyBalances[currentMonth - 1];
				}
			}

			for (int i = 0; i < monthlyBalances.Length; i++)
			{
				Console.WriteLine("Month {0} - Balance: {1}", i + 1, monthlyBalances[i]);
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
