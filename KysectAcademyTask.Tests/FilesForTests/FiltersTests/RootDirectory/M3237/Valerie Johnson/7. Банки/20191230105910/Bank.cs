using System;
using System.Collections.Generic;
using System.Text;

namespace Lab7
{
    public class Bank
    {
        public delegate void BankHandler(string message);
        public event BankHandler Notify;
        private List<Account> accounts;
        private List<Transaction> transactions;
        public string Name { get; }
        public float Comission { get; }
        public int Period { get; }
        public float SumForDoubtful { get; }
        public float CreditLimit { get; private set; }
        public Bank(string name, float comission, int period, float sumForDoubtful, float creditLimit)
        {
            Name = name;
            Comission = comission;
            Period = period;
            SumForDoubtful = sumForDoubtful;
            CreditLimit = -creditLimit;
            accounts = new List<Account>();
            transactions = new List<Transaction>();
            Notify = BankNotify;
        }
        public Account CreateAccount(Client client, Type.AccountType accountType, float sum)
        {
            Account account;
            switch (accountType)
            {
                case Type.AccountType.Credit:
                    account = new CreditAccount(client, sum, Comission, CreditLimit);
                    break;
                case Type.AccountType.Debit:
                    account = new DebitAccount(client, sum);
                    SetAnnualInterest(account);
                    break;
                case Type.AccountType.Deposit:
                    account = new DepositAccount(client, sum, Period);
                    SetAnnualInterest(account);
                    break;
                default:
                    account = new DebitAccount(client, sum);
                    SetAnnualInterest(account);
                    break;
            }
            accounts.Add(account);
            return account;
        }
        public Transaction Add(Account account, float sum)
        {
            Transaction transaction = null;
            if (accounts.Contains(account))
            {
                transaction = account.Add(sum);
                if (transaction != null)
                {
                    transactions.Add(transaction);
                }
            }
            else
            {
                Notify?.Invoke($"Account {account.AccountId} doesn't exist");
            }
            return transaction;
        }
        public Transaction Withdraw(Account account, float sum)
        {
            Transaction transaction = null;
            if (account.IsDoubtful && sum > SumForDoubtful)
            {
                Notify?.Invoke($"Account is doubtful. You cannot withdraw more than {SumForDoubtful}");
            }
            if (accounts.Contains(account))
            {
                transaction = account.Withdraw(sum);
                if (transaction != null)
                {
                    transactions.Add(transaction);
                }
            }
            else
            {
                Notify?.Invoke($"Account {account.AccountId} doesn't exist");
            }
            return transaction;
        }
        public Transaction Transfer(Account from, Account to, float sum)
        {
            Transaction transaction = null;
            if (!accounts.Contains(from))
            {
                Notify?.Invoke($"Account {from.AccountId} doesn't exist");
                return transaction;
            }
            if (!accounts.Contains(to))
            {
                Notify?.Invoke($"Account {to.AccountId} doesn't exist");
                return transaction;
            }
            if (from.IsDoubtful && sum > SumForDoubtful)
            {
                Notify?.Invoke($"Account is doubtful. You cannot exchange more than {SumForDoubtful}");
                return transaction;
            }
            transaction = from.Transfer(to, sum);
            if (transaction != null)
            {
                transactions.Add(transaction);
            }
            return transaction;
        }
        public void UpdateDay()
        {
            foreach (var account in accounts)
            {
                if (account is CreditAccount)
                {
                    continue;
                }
                account.AnnualInterestBonus += account.Balance * account.AnnualInterest / 36500;
            }
        }
        public void UpdateMonth()
        {
            foreach (var account in accounts)
            {
                if (account.AnnualInterestBonus != 0)
                {
                    if (account is CreditAccount)
                    {
                        break;
                    }
                    account.Balance += account.AnnualInterestBonus;
                    account.AnnualInterestBonus = 0;
                }
            }
        }
        private void BankNotify(string message)
        {
            Console.WriteLine(message);
        }
        public void SetAnnualInterest(Account account)
        {
            if (account.Balance <= 0)
            {
                account.AnnualInterest = 0;
            }
            if (account.Balance < 50000)
            {
                account.AnnualInterest = 5;
            }
            else
            {
                if (account.Balance >= 50000 && account.Balance <= 100000)
                {
                    account.AnnualInterest = 5.5F;
                }
                else
                {
                    account.AnnualInterest = 6;
                }
            }
        }
        public void RollBack(Transaction wrongTransaction)
        {
            var transaction = transactions.Find(x => x.TransactionId == wrongTransaction.TransactionId);
            if (transaction != null)
            {
                Notify?.Invoke($"Transaction {transaction.TransactionId} was cancelled");
                if (transaction.From == null)
                {
                    transaction.To.Withdraw(transaction.Sum);
                }
                else
                {
                    if (transaction.To == null)
                    {
                        transaction.From.Add(transaction.Sum);
                    }
                    else
                    {
                        transaction.To.Transfer(transaction.From, transaction.Sum);
                    }
                }
            }
        }
    }

}
