package service;

import entity.AccountID;
import entity.Bank;
import entity.account.Account;

import java.util.ArrayList;
import java.util.List;

public class Network
{
	public static class TransactionException extends Exception{
		public TransactionException(String message) {
			super(message);
		}
	}

	public static final class Access { private Access() {}}
	private static final Access access = new Access();

	public static Network instance = new Network();

	private ArrayList<Transaction> transactions = new ArrayList<>();
	private ArrayList<Bank> banks = new ArrayList<>();

	Bank registerBank(String name) {
		Bank bank = new Bank(banks.size(), name);
		banks.add(bank);
		return bank;
	}

	private Account getAccByID(AccountID id) throws IndexOutOfBoundsException {
		return banks.get(id.bankID).getAccount(access, id.accNumber);
	}

	private Transaction createTransaction(AccountID from, AccountID to, float value) throws TransactionException {
		Account accFrom;
		Account accTo;

		try {
			accFrom = getAccByID(from);
		} catch (IndexOutOfBoundsException e) {
			throw new TransactionException("Invalid sender account details");
		}
		try {
			accTo = getAccByID(to);
		} catch (IndexOutOfBoundsException e) {
			throw new TransactionException("Invalid recipient account details");
		}

		return new Transaction(accFrom, accTo, value);
	}

	public int transfer(AccountID from, AccountID to, float value) throws TransactionException {
		Transaction transaction = createTransaction(from, to, value);
		transaction.exec(access);
		transactions.add(transaction);
		return transactions.size() - 1;
	}

	public boolean cancelTransaction(int transactionID) throws TransactionException {
		try {
			Transaction transaction = transactions.get(transactionID);
			return transaction.cancel(access);

		} catch (IndexOutOfBoundsException e)
		{
			throw new TransactionException("Invalid transaction ID");
		}
	}

	public String getTransactionDetails(int tid) {
		return String.valueOf(transactions.get(tid));
	}

	public void updateBanks() {
		banks.forEach(bank -> bank.update(access));
	}
}
