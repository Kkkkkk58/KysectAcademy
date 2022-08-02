using System;
using System.Collections.Generic;
using System.Text;

namespace Lab7
{
    public class DebitAccount : Account
    {      
        public DebitAccount(Client client, float sum) : base(client, sum)
        {
        }
        public override Transaction Transfer(Account account, float sum)
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
        public override Transaction Withdraw(float sum)
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
