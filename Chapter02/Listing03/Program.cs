using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listing03
{
	class Program
	{
		static void Main(string[] args)
		{
			// Use an Action delegate and a named method
			Task task1 = new Task(new Action<object>(PrintMessage), "First task");

			// Use an anonymous delegate
			Task task2 = new Task(delegate(object obj)
			{
				PrintMessage(obj);
			}, "Second task");

			// Use a lambda expression and a named method
			// Note that lambda parameters to lambda don't need to be quoted if there is only one parameter
			Task task3 = new Task((obj) => PrintMessage(obj), "Third task");

			// Use a lambda expression and an anonymous method
			Task task4 = new Task((obj) =>
			{
				PrintMessage(obj);
			}, "Fourth task");

			task1.Start();
			task2.Start();
			task3.Start();
			task4.Start();

			// Wait for input before existing
			Console.WriteLine("Main method complete. Press enter to finish.");
			Console.ReadLine();
		}

		private static void PrintMessage(object message)
		{
			Console.WriteLine("Message: {0}", message);
		}
	}
}
