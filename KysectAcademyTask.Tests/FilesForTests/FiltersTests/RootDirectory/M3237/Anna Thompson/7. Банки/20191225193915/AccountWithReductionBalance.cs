using LABA_7.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_7.API.Models
{
    public class AccountWithReductionBalance : Account, IAccountWithReductionBalance
    {
        public AccountWithReductionBalance(IClient Client, ITransactionManager TransactionManager, string Id, decimal InterestOnBalance, decimal DoubtfulMaxSum) 
            : base(Client, TransactionManager, Id, InterestOnBalance, DoubtfulMaxSum)
        {

        }


        public override void Recalculation()
        {
            var dateLastInterestAccrual = DateTime.UtcNow.AddSeconds(TimestampLastInterestAccrual);
            var now = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            if (dateLastInterestAccrual <= DateTime.UtcNow.AddMonths(-1))
            {
                if (Balance < 0)
                {
                    var sum = Balance * InterestOnBalance;
                    Balance += sum;
                }

                TimestampLastInterestAccrual = now;
            }
        }
    }
}
