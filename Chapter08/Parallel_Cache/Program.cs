using System;
using System.Threading.Tasks;

namespace Parallel_Cache
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create the cache
			ParallelCache<int, double> cache = new ParallelCache<int, double>(key =>
			{
				Console.WriteLine("Created value for key {0}", key);
				return Math.Pow(key, 2);
			});

			for (int i = 0; i < 10; i++)
			{
				Task.Factory.StartNew(() =>
				{
					for (int j = 0; j < 20; j++)
					{
						Console.WriteLine("Task {0} got value {1} for key {2}",
							Task.CurrentId, cache.GetValue(j), j);
					}
				});
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
