using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Listing06
{
	class Program
	{
		static void Main(string[] args)
		{
			// Cpecify the number of tasks
			int taskCount = 10;

			// Create a countdown event so that we can wait until all of the tasks have been created before breaking
			CountdownEvent cdEvent = new CountdownEvent(taskCount);

			// Create the set of tasks
			Task[] tasks = new Task[taskCount];
			for (int i = 0; i < tasks.Length; i++)
			{
				tasks[i] = Task.Factory.StartNew((stateObj) =>
				{
					// Signal the countdown event
					cdEvent.Signal();

					// Wait on the next task in the array
					tasks[(((int)stateObj) + 1) % taskCount].Wait();
				}, i);
			}

			// Wait for the count down event
			cdEvent.Wait();

			// Break if there is a debugger attached
			if(Debugger.IsAttached)
			{
				Debugger.Break();
			}
		}
	}
}
