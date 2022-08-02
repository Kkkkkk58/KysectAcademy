using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OOPLab7.Accounts
{
    class DebitAccount : Account
    {
        public DebitAccount(Client client, decimal sum = default) : base(client, sum)
        {
        }        

        public override Transaction Transfer(Account account, decimal sum, bool ignore = false)
        {
            if (ignore)
            {
                return base.Transfer(account, sum);
            }
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

        public override Transaction Withdraw(decimal sum, bool ignore = false)
        {
            if (ignore)
            {
                return base.Withdraw(sum);
            }
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
