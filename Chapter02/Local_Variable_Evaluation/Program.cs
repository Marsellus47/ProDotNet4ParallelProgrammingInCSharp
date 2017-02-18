using System;
using System.Threading.Tasks;

namespace Local_Variable_Evaluation
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create and start the "bad" tasks
			for(int i = 0; i < 5; i++)
			{
				Task.Factory.StartNew(() =>
				{
					// Write out a message that uses the loop counter
					Console.WriteLine("Task {0} has counter value: {1}", Task.CurrentId, i);
				});
			}

			// Create and start the "good" tasks
			for (int i = 0; i < 5; i++)
			{
				Task.Factory.StartNew((stateObj) =>
				{
					// Cast the state object to an int
					int loopValue = (int)stateObj;

					// Write out a message that uses the loop counter
					Console.WriteLine("Task {0} has counter value: {1}", Task.CurrentId, loopValue);
				}, i);
			}

			// Wait for input before exiting
			Console.WriteLine("Main method complete. Press enter to finish.");
			Console.ReadLine();
		}
	}
}
