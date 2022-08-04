package entity.account;

import entity.AccountID;
import entity.Client;

public class DebitAccount extends Account
{
	private final float irate_debit;
	private float deferred = 0;

	public DebitAccount(AccountID ID, Client client, float irate_debit) {
		super(ID, client);
		this.irate_debit = irate_debit;
	}

	@Override
	public boolean withdraw(float val)
	{
		if (money >= val) {
			money -= val;
			return true;
		}
		return false;
	}

	@Override
	public void update() {
		super.update();

		deferred += money * irate_debit;

		if (daysActive % 30 == 0) {
			money += deferred;
			deferred = 0;
		}
	}
}
