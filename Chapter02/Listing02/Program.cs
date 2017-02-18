using System;
using System.Threading.Tasks;

namespace Listing02
{
	class Program
	{
		static void Main(string[] args)
		{
			// Use an Action delegate and a named method
			Task task1 = new Task(new Action(PrintMessage));

			// Use an anonymous delegate
			Task task2 = new Task(delegate
			{
				PrintMessage();
			});

			// Use a lambda expression and a named method
			Task task3 = new Task(() => PrintMessage());

			// Use a lambda expression and an anonymous method
			Task task4 = new Task(() =>
			{
				PrintMessage();
			});

			task1.Start();
			task2.Start();
			task3.Start();
			task4.Start();

			// Wait for input before existing
			Console.WriteLine("Main method complete. Press enter to finish.");
			Console.ReadLine();
		}

		private static void PrintMessage()
		{
			Console.WriteLine("Hello World");
		}
	}
}
