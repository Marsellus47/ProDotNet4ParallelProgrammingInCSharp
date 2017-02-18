namespace Listing02
{
	public class ImmutableBankAccount
	{
		public const int AccountNumber = 123456;
		public readonly int Balance;

		public ImmutableBankAccount(int initialBalance)
		{
			Balance = initialBalance;
		}

		public ImmutableBankAccount()
		{
			Balance = 0;
		}
	}
}
