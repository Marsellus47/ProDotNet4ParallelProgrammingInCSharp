using System;
using System.Threading.Tasks;

namespace Listing04
{
	class Program
	{
		static void Main(string[] args)
		{
			string[] messages = { "First task", "Second task", "Third task", "Fourth task" };

			foreach(string msg in messages)
			{
				Task myTask = new Task(obj => PrintMessage((string)obj), msg);
				myTask.Start();
			}

			// Wait for input before existing
			Console.WriteLine("Main method complete. Press enter to finish.");
			Console.ReadLine();
		}

		private static void PrintMessage(string message)
		{
			Console.WriteLine("Message: {0}", message);
		}
	}
}
