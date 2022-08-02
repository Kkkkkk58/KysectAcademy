package service;

import entity.Bank;

public class Service
{
	public static final Service instance = new Service();



	public Bank addBank(String name) {
		return Network.instance.registerBank(name);
	}

	public void countDays(int days) {
		for (int i = 0; i < days; i++)
			Network.instance.updateBanks();
	}

	public String cancelTransaction(int tid) {
		try {
			if (Network.instance.cancelTransaction(tid))
				return "Transaction canceled successfully";
			else
				return "Transaction can not be canceled";
		} catch (Network.TransactionException e) {
			return e.getMessage();
		}
	}
}
