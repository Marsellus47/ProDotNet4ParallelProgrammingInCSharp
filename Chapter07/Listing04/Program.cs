using System;
using System.Threading;
using System.Threading.Tasks;

namespace Listing04
{
	class Program
	{
		static CountdownEvent cdEvent;
		static SemaphoreSlim semA, semB;

		static void Main(string[] args)
		{
			// Initialize the semaphores
			semA = new SemaphoreSlim(2);
			semB = new SemaphoreSlim(2);

			// Define the number of tasks we will use
			int taskCount = 10;

			// Initialize the barrier
			cdEvent = new CountdownEvent(taskCount);

			Task[] tasks = new Task[taskCount];
			for (int i = 0; i < tasks.Length; i++)
			{
				tasks[i] = Task.Factory.StartNew((stateObject) =>
				{
					InitializeMethod((int)stateObject);
				}, i);
			}

			// Wait for all of the tasks to have reached a terminal method
			cdEvent.Wait();

			// Throw an exception to force the debugger to break
			throw new Exception();
		}

		private static void InitializeMethod(int argument)
		{
			if(argument % 2 == 0)
			{
				MethodA(argument);
			}
			else
			{
				MethodB(argument);
			}
		}

		private static void MethodA(int argument)
		{
			if(argument < 5)
			{
				TerminalMethodA();
			}
			else
			{
				TerminalMethodB();
			}
		}

		private static void MethodB(int argument)
		{
			if (argument < 5)
			{
				TerminalMethodA();
			}
			else
			{
				TerminalMethodB();
			}
		}

		private static void TerminalMethodA()
		{
			// Signal the count down event
			cdEvent.Signal();

			// Acquire the lock for this method
			semA.Wait();

			// Perform some work
			for (int i = 0; i < 500000000; i++)
			{
				Math.Pow(i, 2);
			}

			// Release the semaphore
			semA.Release();
		}

		private static void TerminalMethodB()
		{
			// Signal the count down event
			cdEvent.Signal();

			// Acquire the lock for this method
			semB.Wait();

			// Perform some work
			for (int i = 0; i < 500000000; i++)
			{
				Math.Pow(i, 3);
			}

			// Release the semaphore
			semB.Release();
		}
	}
}
