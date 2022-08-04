package entity.account;

import entity.AccountID;
import service.Network;

public class AccountRemote {
	private final Account account;

	public float balance() {
		return account.getMoney();
	}

	public AccountRemote(Account account) {
		this.account = account;
	}

	public boolean withdraw(float value) {
		return account.withdraw(value);
	}

	public void deposit(float value) {
		account.deposit(value);
	}

	public String transfer(AccountID toID, float value) {
		try {
			int tid = account.transfer(toID, value);
			return "Transfer complete. " + Network.instance.getTransactionDetails(tid) + " Transaction ID: " + tid;

		} catch (Network.TransactionException e) {
			return "Transfer failed: " + e.getMessage();
		}
	}
}
