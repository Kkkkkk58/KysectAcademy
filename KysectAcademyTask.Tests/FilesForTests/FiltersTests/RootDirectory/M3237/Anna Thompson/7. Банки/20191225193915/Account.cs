using LABA_7.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_7.API.Models
{
    public abstract class Account : IAccount
    {
        public Account(IClient Client, ITransactionManager TransactionManager, string Id, decimal InterestOnBalance, decimal DoubtfulMaxSum)
        {
            this.Id = Id;
            this.Client = Client;
            this.TransactionManager = TransactionManager;
            this.InterestOnBalance = InterestOnBalance;
            this.DoubtfulMaxSum = DoubtfulMaxSum;
            TimestampLastInterestAccrual = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        }
        public string Id { get; set; }
        public int BankId { get; set; }
        public decimal Balance { get; set; }
        public int TimestampLastInterestAccrual { get; set; }
        protected ITransactionManager TransactionManager { get; set; }
        public IClient Client { get; set; }
        public decimal InterestOnBalance { get; set; }
        private decimal DoubtfulMaxSum { get; set; }

        public virtual ITransaction Replenish(decimal Sum)
        {
            return TransactionManager.CreateTransaction(Sum, this, TransactionType.Replenish);
        }

        public virtual ITransaction Transfer(decimal Sum, string AccountId)
        {
            if (!Client.Doubtful || Sum <= DoubtfulMaxSum)
                return TransactionManager.CreateTransaction(Sum, this, TransactionType.Transfer, AccountId);
            else
                Logger.Error("Not valid transfer for doubtful account");
            return null;
        }

        public virtual ITransaction Withdrawal(decimal Sum)
        {
            if (!Client.Doubtful || Sum <= DoubtfulMaxSum)
                return TransactionManager.CreateTransaction(Sum, this, TransactionType.Withdrawal);
            else
                Logger.Error("Not valid transfer for doubtful account");
            return null;
        }

        public abstract void Recalculation();
    }
}
