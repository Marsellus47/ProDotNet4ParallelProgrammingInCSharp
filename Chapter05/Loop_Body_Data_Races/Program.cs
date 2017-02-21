using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Loop_Body_Data_Races
{
	class Program
	{
		static void Main(string[] args)
		{
			Stopwatch stopwatch = new Stopwatch();
			int numberOfItems = 1000000;

			// Create the shared data value
			long total = 0;

			// TOTALLY BAD: Perform a parallel loop
			stopwatch.Start();
			Parallel.For(
				0,
				numberOfItems,
				item =>
				{
					total += (long)Math.Pow(item, 2);
				});
			stopwatch.Stop();
			Console.WriteLine("Elapsed {0} milliseconds", stopwatch.ElapsedMilliseconds);
			Console.WriteLine("Expected result: 333332833333500000");
			Console.WriteLine("Actual result:   {0}", total);
		}
	}
}
