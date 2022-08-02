using LABA_7.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_7.API.Models
{
    public class DebitAccount : AccountWithIncreasingBalance, IDebitAccount
    {
        public DebitAccount(IClient Client, ITransactionManager TransactionManager, string Id, decimal InterestOnBalance, decimal DoubtfulMaxSum) 
            : base(Client, TransactionManager, Id, InterestOnBalance, DoubtfulMaxSum)
        {
        }

        public override ITransaction Transfer(decimal Sum, string AccountId)
        {
            if (Balance >= Sum)
                return base.Transfer(Sum, AccountId);
            Logger.Error("Not enought balance");
            return null;
        }

        public override ITransaction Withdrawal(decimal Sum)
        {
            if (Balance >= Sum)
                return base.Withdrawal(Sum);
            Logger.Error("Not enought balance");
            return null;
        }
    }
}
