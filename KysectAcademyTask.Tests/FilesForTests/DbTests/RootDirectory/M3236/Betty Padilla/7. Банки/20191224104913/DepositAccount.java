package entity.account;

import entity.AccountID;
import entity.Client;
import javafx.util.Pair;

import java.util.ArrayList;
import java.util.List;

public class DepositAccount extends Account
{
	private final int daysDeposit;
	private final List<Pair<Float, Float>> irate_deposit;
	private int deferred = 0;

	public DepositAccount(AccountID ID, Client client, int daysDeposit, List<Pair<Float, Float>> irate_deposit) {
		super(ID, client);
		this.daysDeposit = daysDeposit;
		this.irate_deposit = new ArrayList<>(irate_deposit);
	}

	@Override
	public boolean withdraw(float val) {
		if (daysActive > daysDeposit)
			if (money >= val) {
				money -= val;
				return true;
			}
		return false;
	}


	private float getIRate()
	{
		float sum = 0;
		for (Pair<Float, Float> p : irate_deposit)
		{
			sum += p.getKey();

			if (money <= sum)
			{
				return p.getValue();
			}
		}

		return irate_deposit.get(irate_deposit.size() - 1).getValue();
	}

	@Override
	public void update() {
		super.update();

		deferred += money * getIRate();
		//System.out.println("!!!"+getIRate());
		if (daysActive % 30 == 0) {
			money += deferred;
			deferred = 0;
		}
	}
}
