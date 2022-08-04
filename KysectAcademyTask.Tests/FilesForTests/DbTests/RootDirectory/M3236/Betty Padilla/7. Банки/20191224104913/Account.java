package entity.account;

import entity.AccountID;
import entity.Bank;
import entity.Client;
import service.Network;

public abstract class Account
{
//	public static class OperationException extends Exception {
//		public OperationException(String message) {
//			super(message);
//		}
//	}

	public final AccountID ID;
	protected float money;

	public float getMoney() {
		return money;
	}

	protected final Client client;
	protected int daysActive;

	protected Account(AccountID ID, Client client) {
		this.ID = ID;
		this.client = client;
	}

	public void update()
	{
		daysActive += 1;
	}

	public int transfer(AccountID toID, float val) throws Network.TransactionException {
		return Network.instance.transfer(ID, toID, val);
	}

	public void deposit(float val) {
		money += val;
	}

	public abstract boolean withdraw(float val);
}
