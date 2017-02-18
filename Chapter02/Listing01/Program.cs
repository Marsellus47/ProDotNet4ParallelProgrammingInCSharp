using System;
using System.Threading.Tasks;

namespace Listing01
{
	class Program
	{
		static void Main(string[] args)
		{
			Task.Factory.StartNew(() =>
			{
				Console.WriteLine("Hello World");
			});

			// Wait for input before exiting
			Console.WriteLine("Main method complete. Press enter to finish.");
			Console.ReadLine();
		}
	}
}
