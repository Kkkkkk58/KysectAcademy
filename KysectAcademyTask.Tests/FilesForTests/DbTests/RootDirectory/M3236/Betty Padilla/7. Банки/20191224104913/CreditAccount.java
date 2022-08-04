package entity.account;

import entity.AccountID;
import entity.Client;

public class CreditAccount extends Account
{
	private boolean fine = false;
	private final float creditFineValue;
	private final float creditLimit;

	public CreditAccount(AccountID ID, Client client, float creditFineValue, float creditLimit) {
		super(ID, client);
		this.creditFineValue = creditFineValue;
		this.creditLimit = creditLimit;
	}

	@Override
	public boolean withdraw(float val) {
		if (money - val >= -creditLimit) {
			money -= val;
			if (money < 0)
				fine = true;

			return true;
		}

		return false;
	}

	@Override
	public void update() {
		super.update();

		if (daysActive % 30 == 0) {
			if (fine)
			{
				fine = false;
				money -= creditFineValue;
				if (money < 0)
					fine = true;
			}
		}
	}
}
