using System;
using System.Threading.Tasks;

namespace Listing12
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a running total of matched words
			int matchedWords = 0;

			// Create a lock object
			object lockObj = new object();

			// Define the source data
			string[] dataItems = new string[] { "an", "apple", "a", "day", "keeps", "the", "doctor", "away" };

			// Perform a parallel foreach loop with TLS
			Parallel.ForEach(
				dataItems,
				() => 0,
				(string item, ParallelLoopState loopState, int tlsValue) =>
				{
					// Increment the tls value if the item contains the letter 'a'
					if(item.Contains("a"))
					{
						tlsValue++;
					}
					return tlsValue;
				},
				tlsValue =>
				{
					lock(lockObj)
					{
						matchedWords += tlsValue;
					}
				});

			Console.WriteLine("Matches: {0}", matchedWords);

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
