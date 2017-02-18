using System;
using System.Threading.Tasks;

namespace Listing23
{
	class Program
	{
		static void Main(string[] args)
		{
			// Define the function
			Func<string> taskBody = new Func<string>(() =>
			{
				Console.WriteLine("Task body 1 working...");
				return "Task 1 Result";
			});

			// Create the lazy variable
			Lazy<Task<string>> lazyData = new Lazy<Task<string>>(() =>
				Task<string>.Factory.StartNew(taskBody));

			Console.WriteLine("Calling lazy variable");
			Console.WriteLine("Result from task 1: {0}", lazyData.Value.Result);

			// Do the same thing in a single statement
			Lazy<Task<string>> lazyData2 = new Lazy<Task<string>>(() =>
				Task<string>.Factory.StartNew(() =>
				{
					Console.WriteLine("Task body 2 working...");
					return "Task 2 Result";
				}));

			Console.WriteLine("Calling second lazy variable");
			Console.WriteLine("Result from task 2: {0}", lazyData2.Value.Result);

			// Wait for input before exiting
			Console.WriteLine("Main method complete. Press enter to finish.");
			Console.ReadLine();
		}
	}
}
