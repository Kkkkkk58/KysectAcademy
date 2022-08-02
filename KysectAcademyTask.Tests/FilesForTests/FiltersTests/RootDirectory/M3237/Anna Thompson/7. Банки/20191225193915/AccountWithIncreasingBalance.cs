using LABA_7.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_7.API.Models
{
    public class AccountWithIncreasingBalance : Account, IAccountWithIncreasingBalance
    {
        public AccountWithIncreasingBalance(IClient Client, ITransactionManager TransactionManager, string Id, decimal InterestOnBalance, decimal DoubtfulMaxSum) 
            : base(Client, TransactionManager, Id, InterestOnBalance, DoubtfulMaxSum)
        {
            TimestampLastMonthInterestAccrual = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public decimal UnaddressedBalance { get; set; }
        public int TimestampLastMonthInterestAccrual { get; set; }

        public override void Recalculation()
        {
            var now = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            if (now - TimestampLastInterestAccrual >= 86400)
            {
                if (Balance > 0)
                {
                    var sum = Balance * (InterestOnBalance / 365);
                    UnaddressedBalance += sum;
                }

                TimestampLastInterestAccrual = now;
            }

            var dateLastInterestAccrual = DateTime.UtcNow.AddSeconds(TimestampLastMonthInterestAccrual);
            if (dateLastInterestAccrual <= DateTime.UtcNow.AddMonths(-1))
            {
                if (UnaddressedBalance > 0)
                {
                    Balance += UnaddressedBalance;
                    UnaddressedBalance = 0;
                }

                TimestampLastMonthInterestAccrual = now;
            }
        }
    }
}
