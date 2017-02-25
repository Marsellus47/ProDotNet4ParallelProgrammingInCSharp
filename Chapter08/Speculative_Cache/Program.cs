using System;
using System.Linq;

namespace Speculative_Cache
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a new instance of the cache
			SpeculativeCache<int, double> cache = new SpeculativeCache<int, double>(
				key1 =>
				{
					Console.WriteLine("Created value for key {0}", key1);
					return Math.Pow(key1, 2);
				},
				key2 => Enumerable.Range(key2 + 1, 5).ToArray());

			// Request some values from the cache
			for (int i = 0; i < 100; i++)
			{
				double value = cache.GetValue(i);
				Console.WriteLine("Got result {0} for key {1}",
					value, i);
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
