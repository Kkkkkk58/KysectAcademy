using System;
using System.Collections.Generic;
using System.Text;

namespace Lab7
{
    class DepositAccount : Account
    {
        public DepositAccount(Client client, float sum, int period) : base(client, sum)
        {
            Period = period;
        }
        public int Period { get; set; }
        public override Transaction Transfer(Account account, float sum)
        {
            if (Period > 0)
            {
                OnUpdate($"Period of account {AccountId} is not ended");
                return null;
            }
            else
            {
                if (Balance < sum)
                {
                    OnUpdate($"Not enough money to transfer {sum} from account {AccountId}");
                    return null;

                }
                else
                {
                    return base.Transfer(account, sum);
                }
            }
        }
        public override Transaction Withdraw(float sum)
        {
            if (Period > 0)
            {
                OnUpdate($"Period of account {AccountId} is not ended");
                return null;
            }
            else
            {
                if (Balance >= sum)
                {
                    return base.Withdraw(sum);
                }
                else
                {
                    OnUpdate($"Not enough money to withdraw {sum} from account {AccountId}");
                    return null;
                }
            }
        }
    }
}
