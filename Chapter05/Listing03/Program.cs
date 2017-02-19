using System;
using System.Threading.Tasks;

namespace Listing03
{
	class Program
	{
		static void Main(string[] args)
		{
			Parallel.For(0, 10, index =>
			{
				Console.WriteLine("Task ID {0} processing index: {1}", Task.CurrentId, index);
			});

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
