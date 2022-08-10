using System;
using System.Collections.Generic;
using System.Text;
using OOPLab7.Accounts;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using OOPLab7.Observers;

namespace OOPLab7
{
    enum AccountType
    {
        Debit,
        Saving,
        Credit
    }
    class Bank : ISubscriber
    {
        public delegate void BankHandler(string message);
        public event BankHandler Notify;
        private SortedSet<Account> _accounts;
        private List<Transaction> _transactions;
        public Bank(string name, decimal interestRate = default, int period = default, decimal sumForDoubtful = default, decimal creditLimit = default)
        {
            Name = name;
            InterestRate = Math.Abs(interestRate);
            Period = period;
            SumForDoubtful = sumForDoubtful;
            CreditLimit = -Math.Abs(creditLimit);
            _accounts = new SortedSet<Account>();
            _transactions = new List<Transaction>();
            Notify = Bank_Notify;
            ChargeInterest = SetAnnualInterest;
            BankTimer.AddSubscriber(this);
        }
        public string Name { get; }
        public decimal InterestRate { get; }
        public int Period { get; }
        public decimal SumForDoubtful { get; }
        public decimal CreditLimit { get; private set; }
        public decimal AnnualInterest { get; private set; }
        public Action<decimal> ChargeInterest;
        private void Bank_Notify(string message)
        {
            Console.WriteLine(message);
        }
        public Account CreateAccount(Client client, AccountType accountType , decimal sum = default)
        {
            var result = new List<ValidationResult>();
            var context = new ValidationContext(client);
            if (!Validator.TryValidateObject(client, context, result, true))
            {
                foreach (var error in result)
                {
                    Notify?.Invoke(error.ErrorMessage);
                }
                return null;
            }
            Account account;
            switch (accountType)
            {
                case AccountType.Credit:
                    account = new CreditAccount(client, sum, InterestRate, CreditLimit);
                    break;
                case AccountType.Debit:
                    account = new DebitAccount(client, sum);
                    break;
                case AccountType.Saving:
                    account = new SavingAccount(client, sum, Period);
                    break;
                default:
                    account = new DebitAccount(client, sum);
                    break;
            }
            _accounts.Add(account);
            return account;
        }
        public Transaction Add(Account account, decimal sum)
        {
            Transaction transaction = null;
            if (_accounts.Contains(account))
            {
                transaction = account.Add(sum);
                if (transaction != null)
                {
                    _transactions.Add(transaction);
                }
            }
            else
            {
                Notify?.Invoke($"Account {account?.AccountId} doesn't exist");
            }
            return transaction;
        }
        public Transaction Withdraw(Account account, decimal sum)
        {
            Transaction transaction = null;
            if (account.IsDoubtful && sum > SumForDoubtful)
            {
                Notify?.Invoke($"Account is doubtful. You cannot withdraw more than {SumForDoubtful:C}");                
            }
            if (_accounts.Contains(account))
            {
                transaction = account.Withdraw(sum);
                if (transaction != null)
                {
                    _transactions.Add(transaction);
                }
            }
            else
            {
                Notify?.Invoke($"Account {account.AccountId} doesn't exist");
            }
            return transaction;
        }
        public Transaction Transfer(Account from, Account to, decimal sum)
        {
            Transaction transaction = null;
            if (!_accounts.Contains(from))
            {
                Notify?.Invoke($"Account {from.AccountId} doesn't exist");
                return transaction;
            }
            if (!_accounts.Contains(to))
            {
                Notify?.Invoke($"Account {to.AccountId} doesn't exist");
                return transaction;
            }
            if (from.IsDoubtful && sum > SumForDoubtful)
            {
                Notify?.Invoke($"Account is doubtful. You cannot exchange more than {SumForDoubtful:C}");
                return transaction;
            }
            transaction = from.Transfer(to, sum);
            if (transaction != null)
            {
                _transactions.Add(transaction);
            }
            return transaction;
        }
        public void GetBalance(Account account)
        {
            Notify?.Invoke($"Balance of account {account.AccountId} is {account.Balance:C}");
        }

        public void UpdateDay()
        {
            foreach (var account in _accounts)
            {
                if (account is CreditAccount)
                {
                    continue;
                }
                ChargeInterest(account.Balance);
                account.AnnualInterestBonus += account.Balance * AnnualInterest / 36500;
            }
        }

        public void UpdateMonth()
        {
            foreach(var account in _accounts)
            {
                if (account.AnnualInterestBonus != 0)
                {
                    if (account is CreditAccount)
                    {
                        continue;
                    }
                    account.Add(account.AnnualInterestBonus);
                    account.AnnualInterestBonus = 0;
                }
            }
        }
        public void SetAnnualInterest(decimal sum)
        {
            if (sum <= 0)
            {
                AnnualInterest = 0;
                return;
            }
            if (sum < 50000)
            {
                AnnualInterest = 5;
            }
            else
            {
                if (sum >= 50000 && sum <= 100000)
                {
                    AnnualInterest = 5.5m;
                }
                else
                {
                    AnnualInterest = 6;
                }
            }
        }
        public void RollBack(Transaction wrongTransaction)
        {
            var transaction = _transactions.Find(x => x.CompareTo(wrongTransaction) == 0);
            if (transaction != null)
            {
                Notify?.Invoke($"Transaction {transaction.TransactionId} is cancelled");
                if (transaction.From == null)
                {
                    transaction.To.Withdraw(transaction.Sum, true);
                }
                else
                {
                    if (transaction.To == null)
                    {
                        transaction.From.Add(transaction.Sum, true);
                    }
                    else
                    {
                        transaction.To.Transfer(transaction.From, transaction.Sum, true);
                    }
                }
            }
        }
    }
}
