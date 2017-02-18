using System;
using System.Runtime.Remoting.Contexts;

namespace Listing14
{
	[Synchronization]
	public class BankAccount : ContextBoundObject
	{
		private int balance = 0;

		public void IncrementBalance()
		{
			balance++;
		}

		public int GetBalance()
		{
			return balance;
		}
	}
}
