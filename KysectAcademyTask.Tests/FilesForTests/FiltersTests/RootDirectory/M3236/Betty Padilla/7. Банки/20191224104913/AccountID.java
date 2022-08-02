package entity;

public class AccountID
{
	private static final int MAX_CLIENTS_PER_BANK = 1000;

	public final int bankID;
	public final int accNumber;

	public AccountID(int bankID, int accNumber) {
		this.bankID = bankID;
		this.accNumber = accNumber;
	}

	public AccountID(int rawID) {
		bankID = rawID / MAX_CLIENTS_PER_BANK;
		accNumber = rawID % MAX_CLIENTS_PER_BANK;
	}

	int get() {
		return bankID * MAX_CLIENTS_PER_BANK + accNumber;
	}

	@Override
	public String toString() {
		return String.valueOf(get());
	}
}
