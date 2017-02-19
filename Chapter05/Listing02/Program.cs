using System;
using System.Threading.Tasks;

namespace Listing02
{
	class Program
	{
		static void Main(string[] args)
		{
			// Invoke actions described by lambda expressions
			Parallel.Invoke(
				() => Console.WriteLine("Action 1"),
				() => Console.WriteLine("Action 2"),
				() => Console.WriteLine("Action 3"));

			// Explicitly create an array of actions
			Action[] actions = new Action[3];
			actions[0] = new Action(() => Console.WriteLine("Action 4"));
			actions[1] = new Action(() => Console.WriteLine("Action 5"));
			actions[2] = new Action(() => Console.WriteLine("Action 6"));

			// Invoke the actions array
			Parallel.Invoke(actions);

			// Create the same effect using tasks explicitly
			Task parent = Task.Factory.StartNew(() =>
			{
				foreach (var action in actions)
				{
					Task.Factory.StartNew(action, TaskCreationOptions.AttachedToParent);
				}
			});

			// Wait for the task to finish
			parent.Wait();

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
