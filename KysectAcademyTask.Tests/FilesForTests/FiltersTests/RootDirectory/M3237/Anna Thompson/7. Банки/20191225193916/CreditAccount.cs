using LABA_7.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_7.API.Models
{
    public class CreditAccount : AccountWithReductionBalance, ICreditAccount
    {
        public CreditAccount(IClient Client, ITransactionManager TransactionManager, string Id, 
            decimal InterestOnBalance, decimal DoubtfulMaxSum, decimal CreditLimit, decimal CreditAccountComission) 
            : base(Client, TransactionManager, Id, InterestOnBalance, DoubtfulMaxSum)
        {
            this.CreditLimit = CreditLimit;
            this.CreditAccountComission = CreditAccountComission;
        }

        public decimal CreditLimit { get; set; }
        public decimal CreditAccountComission { get; set; }

        public override ITransaction Transfer(decimal Sum, string AccountId)
        {
            if (Balance < 0)
                Sum += CreditAccountComission;
            if (Balance - Sum >= CreditLimit)
                return base.Transfer(Sum, AccountId);
            Logger.Error("Not enought balance");
            return null;
        }

        public override ITransaction Withdrawal(decimal Sum)
        {
            if (Balance < 0)
                Sum += CreditAccountComission;
            if (Balance - Sum >= CreditLimit)
                return base.Withdrawal(Sum);
            Logger.Error("Not enought balance");
            return null;
        }
    }
}
