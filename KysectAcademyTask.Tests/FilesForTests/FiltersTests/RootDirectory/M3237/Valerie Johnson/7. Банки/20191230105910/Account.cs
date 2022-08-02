using System;
using System.Collections.Generic;
using System.Text;

namespace Lab7
{
    abstract public class Account
    {
        public delegate void AccountHandler(string message);
        public event AccountHandler Notify;
        public Guid AccountId { get; }
        public Client Client { get; }
        public float Balance { get; set; }
        public bool IsDoubtful { get; protected set; }
        public float AnnualInterestBonus { get; set; } = 0;
        public float AnnualInterest { get; set; } = 0;
        public Account(Client client, float sum)
        {
            AccountId = Guid.NewGuid();
            Client = client;
            Balance = sum;
            if (client.Address == null || client.Passport == null)
            {
                IsDoubtful = true;
            }
            Notify = AccountNotify;
        }
        protected void AccountNotify(string message)
        {
            Console.WriteLine(message);
        }
        protected void OnUpdate(string message)
        {
            Notify?.Invoke(message);
        }
        public virtual Transaction Withdraw(float sum)
        {
            Balance -= sum;
            Notify?.Invoke($"{sum} was withdrawn from account {AccountId}");
            return new Transaction(this, null, sum);
        }
        public virtual Transaction Transfer(Account account, float sum)
        {
            Balance -= sum;
            account.Balance += sum;
            Notify?.Invoke($"Transfer on sum {sum} between {AccountId} and {account.AccountId} was successfully completed");
            return new Transaction(this, account, sum);
        }
        public Transaction Add(float sum)
        {
            Balance += sum;
            Notify?.Invoke($"{sum} was injected into account {AccountId}");
            return new Transaction(null, this, sum);
        }
    }
}
