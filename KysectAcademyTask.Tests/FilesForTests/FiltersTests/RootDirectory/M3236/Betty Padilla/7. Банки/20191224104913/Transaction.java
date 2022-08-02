package service;

import entity.account.Account;

public class Transaction
{
	final Account from;
	final Account to;
	final float value;

	private boolean valid = false;

	public Transaction(Account from, Account to, float value) {
		this.from = from;
		this.to = to;
		this.value = value;
	}

	boolean exec(Network.Access access) throws Network.TransactionException
	{
		if (!valid)
		{
			if (!from.withdraw(value))
				throw new Network.TransactionException("Can't withdraw money from sender");
			to.deposit(value);
			valid = true;
		}
		return false;
	}

	boolean cancel(Network.Access access)
	{
		if (valid)
		{
			if (!to.withdraw(value))
				return false;
			from.deposit(value);
			valid = false;
			return true;
		}
		return false;
	}

	@Override
	public String toString() {
		return "Transaction{" +
				"from=" + from.ID +
				", to=" + to.ID +
				", value=" + value +
				", valid=" + valid +
				'}';
	}
}
