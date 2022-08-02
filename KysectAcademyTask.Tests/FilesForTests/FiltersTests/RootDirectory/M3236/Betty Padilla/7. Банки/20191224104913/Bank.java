package entity;

import entity.account.*;
import javafx.util.Pair;
import service.Network;

import java.util.ArrayList;
import java.util.List;

public class Bank {
	public final int bankID;
	public final String name;

	private float irate_debit;

	public void setDebitRules(float irate_debit) {
		this.irate_debit = irate_debit;
	}

	private List<Pair<Float, Float>> irate_deposit;
	private int daysDeposit;

	public void setDepositRules(List<Pair<Float, Float>> irate_deposit, int daysDeposit){
		this.irate_deposit = new ArrayList<>(irate_deposit);
		this.daysDeposit = daysDeposit;
	}

	private float creditFineValue;
	private float creditLimit;

	public void setCreditRules(float creditFineValue, float creditLimit) {
		this.creditFineValue = creditFineValue;
		this.creditLimit = creditLimit;
	}

	private List<Account> accounts = new ArrayList<>();
	private List<Client> clients = new ArrayList<>();

	public int registerClient(String name, String address, Long passportID) {
		int id = clients.size();
		clients.add(new Client(name, address, passportID));
		return id;
	}


	public AccountID openDebitAccount(int clientID) {
		AccountID id = new AccountID(bankID, accounts.size());
		accounts.add(new DebitAccount(id, clients.get(clientID), irate_debit));
		return id;
	}

	public AccountID openCreditAccount(int clientID) {
		AccountID id = new AccountID(bankID, accounts.size());
		accounts.add(new CreditAccount(id, clients.get(clientID), creditFineValue, creditLimit));
		return id;
	}

	public AccountID openDepositAccount(int clientID) {
		AccountID id = new AccountID(bankID, accounts.size());
		accounts.add(new DepositAccount(id, clients.get(clientID), daysDeposit, irate_deposit));
		return id;
	}

	public Account getAccount(Network.Access access, int accNumber) throws ArrayIndexOutOfBoundsException {
		return accounts.get(accNumber);
	}

	public void update(Network.Access access) {
		accounts.forEach(Account::update);
	}

	public AccountRemote login(AccountID id) {
		if (id.bankID != bankID)
			return null;
		return new AccountRemote(accounts.get(id.accNumber));
	}


	public Bank(int bankID, String name) {
		this.bankID = bankID;
		this.name = name;
	}
}
