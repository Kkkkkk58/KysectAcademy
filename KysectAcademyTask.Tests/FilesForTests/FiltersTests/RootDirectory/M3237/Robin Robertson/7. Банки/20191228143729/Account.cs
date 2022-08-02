using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using OOPLab7.Observers;

namespace OOPLab7.Accounts
{
    abstract class Account : IComparable<Account>
    {
        public delegate void BillHandler(string message);
        public event BillHandler Notify;
        public Account(Client client, decimal sum)
        {
            AccountId = Guid.NewGuid();
            Client = client;
            Balance = sum;
            if (client.Address == null || client.Passport == null)
            {
                IsDoubtful = true;
            }
            Notify = Bill_Notify;
        }

        

        public Guid AccountId { get; }
        public Client Client { get; }
        public decimal Balance { get; protected set; }
        public bool IsDoubtful { get; protected set; }
        public decimal AnnualInterestBonus { get; set; } = 0;

        public virtual Transaction Withdraw(decimal sum, bool ignore = false)
        {
            Balance -= sum;
            Notify?.Invoke($"{sum:C} was withdrawn from account {AccountId}");
            return new Transaction(this, null, sum);
        }
        public virtual Transaction Transfer(Account account, decimal sum, bool ignore = false)
        {
            Balance -= sum;
            account.Balance += sum;
            Notify?.Invoke($"Transfer on sum {sum:C} between {AccountId} and {account.AccountId} was successfully completed");
            return new Transaction(this, account, sum);
        }
        public Transaction Add(decimal sum, bool ignore = false)
        {
            Balance += sum;
            Notify?.Invoke($"{sum:C} was injected into account {AccountId}");
            return new Transaction(null, this, sum);
        }
        protected void Bill_Notify(string message)
        {
            Console.WriteLine(message);
        }
        protected virtual void OnUpdate(string message)
        {
            Notify?.Invoke(message);
        }

        public int CompareTo([AllowNull] Account other)
        {
            return AccountId.CompareTo(other.AccountId);
        }

        
    }
}
