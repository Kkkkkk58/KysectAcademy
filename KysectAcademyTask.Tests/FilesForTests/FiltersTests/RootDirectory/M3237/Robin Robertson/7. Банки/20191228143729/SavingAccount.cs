using System;
using System.Collections.Generic;
using System.Text;

namespace OOPLab7.Accounts
{
    
    class SavingAccount : Account
    {
        public SavingAccount(Client client, decimal sum, int period) : base(client, sum)
        {
            Period = period < 0 ? 0 : period;
        }

        public int Period { get; set; }

        public override Transaction Transfer(Account account, decimal sum, bool ignore = false)
        {
            if (ignore)
            {
                return base.Transfer(account, sum);
            }
            if (Period > 0)
            {
                OnUpdate($"Period of account {AccountId} is not ended");
                return null;
            }
            else
            {
                if (Balance < sum)
                {
                    OnUpdate($"Not enough money to transfer {sum:C} from account {AccountId}");
                    return null;

                }
                else
                {
                    return base.Transfer(account, sum);
                }
            }
        }

        public override Transaction Withdraw(decimal sum, bool ignore = false)
        {
            if (ignore)
            {
                return base.Withdraw(sum);
            }
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
                    OnUpdate($"Not enough money to withdraw {sum:C} from account {AccountId}");
                    return null;
                }
            }
        }
    }
}
