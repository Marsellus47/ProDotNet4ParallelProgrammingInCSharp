using System;
using System.Linq;

namespace Confusing_Ordering
{
	class Program
	{
		static void Main(string[] args)
		{
			string[] sourceData = new string[] { "an", "apple", "a", "day", "keeps", "the", "doctor", "away" };

			// Create an AsOrdered() query
			var result1 = sourceData
				.AsParallel()
				.AsOrdered()
				.Select(item => item);

			// Enumerate the results
			foreach (var item in result1)
			{
				Console.WriteLine("As Ordered() - {0}", item);
			}

			// Create an OrderBy() query
			var result2 = sourceData
				.AsParallel()
				.OrderBy(item => item)
				.Select(item => item);

			// Enumerate the results
			foreach (var item in result2)
			{
				Console.WriteLine("As OrderBy() - {0}", item);
			}
		}
	}
}
