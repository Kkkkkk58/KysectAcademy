using LABA_7.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_7.API.Models
{
    public class DepositAccount : AccountWithIncreasingBalance, IDepositAccout
    {
        public DepositAccount(IClient Client, ITransactionManager TransactionManager, string Id, decimal InterestOnBalance, decimal DoubtfulMaxSum, int TimestampExpiration) 
            : base(Client, TransactionManager, Id, InterestOnBalance, DoubtfulMaxSum)
        {
            this.TimestampExpiration = TimestampExpiration;
        }

        public int TimestampExpiration { get; set; }

        public override ITransaction Transfer(decimal Sum, string AccountId)
        {
            var now = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            if (now > TimestampExpiration)
            {
                if (Balance >= Sum)
                    return base.Transfer(Sum, AccountId);
                else
                    Logger.Error("Not enought balance");
            }
            Logger.Error("The card has not expired");
            return null;
        }

        public override ITransaction Withdrawal(decimal Sum)
        {
            var now = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            if (now > TimestampExpiration)
            {
                if (Balance >= Sum)
                    return base.Withdrawal(Sum);
                else
                    Logger.Error("Not enought balance");
            }
            Logger.Error("The card has not expired");
            return null;
        }
    }
}
