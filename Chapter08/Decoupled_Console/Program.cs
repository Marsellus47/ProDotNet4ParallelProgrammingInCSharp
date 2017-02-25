using System;
using System.Threading.Tasks;

namespace Decoupled_Console
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create a set of tasks that each writes messages
			for (int i = 0; i < 10; i++)
			{
				Task.Factory.StartNew(state =>
				{
					for (int j = 0; j < 10; j++)
					{
						DecoupledConsole.WriteLine("Message {1} from task {0}", Task.CurrentId, j);
					}
				}, i);
			}

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}
	}
}
