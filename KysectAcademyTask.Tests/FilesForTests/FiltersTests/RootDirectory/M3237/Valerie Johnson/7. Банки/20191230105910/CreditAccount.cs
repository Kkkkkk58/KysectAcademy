using System;
using System.Collections.Generic;
using System.Text;

namespace Lab7
{
    public class CreditAccount : Account
    {
        public CreditAccount(Client client, float sum, float comission, float creditLimit) : base(client, sum)
        {
            Comission = comission;
            CreditLimit = creditLimit;
        }
        public float Comission { get; }
        public float CreditLimit { get; }

        public override Transaction Transfer(Account account, float sum)
        {
            if (CreditLimit > Balance - sum)
            {
                OnUpdate($"You cannot withdraw a sum {sum}. Credit limit is {CreditLimit}");
                return null;
            }
            if (Balance < 0)
            {
                WithdrawComission(sum);
            }
            return base.Transfer(account, sum);
        }

        public override Transaction Withdraw(float sum)
        {
            if (CreditLimit > Balance - sum)
            {
                OnUpdate($"You cannot withdraw a sum {sum}. Credit limit is {CreditLimit}");
                return null;
            }
            if (Balance < 0)
            {
                WithdrawComission(sum);
            }
            return base.Withdraw(sum);
        }
        private void WithdrawComission(float sum)
        {
            float addition = sum * Comission / 100;
            OnUpdate($"Balance is negative. Interest rate: {addition}");
            base.Withdraw(addition);
        }
    }
}
