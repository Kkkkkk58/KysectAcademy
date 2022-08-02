using System;
using System.Collections.Generic;
using System.Text;

namespace OOPLab7.Accounts
{
    class CreditAccount : Account
    {
        public CreditAccount(Client client, decimal sum, decimal interestRate, decimal creditLimit) : base(client, sum)
        {            
            InterestRate = interestRate;
            CreditLimit = creditLimit;
        }
        public decimal InterestRate { get; }
        public decimal CreditLimit { get; }

        public override Transaction Transfer(Account account, decimal sum, bool ignore = false)
        {
            if (ignore)
            {
                return base.Transfer(account, sum);
            }
            if (CreditLimit > Balance - sum)
            {
                OnUpdate($"You cannot withdraw a sum {sum:C}. Credit limit is {CreditLimit:C}");
                return null;
            }
            if (Balance < 0)
            {
                AddInterestRate(sum);                
            }
            return base.Transfer(account, sum);
        }

        public override Transaction Withdraw(decimal sum, bool ignore = false)
        {
            if (ignore)
            {
                return base.Withdraw(sum);
            }
            if (CreditLimit > Balance - sum)
            {
                OnUpdate($"You cannot withdraw a sum {sum:C}. Credit limit is {CreditLimit:C}");
                return null;
            }
            if (Balance < 0)
            {
                AddInterestRate(sum);
            }
            return base.Withdraw(sum);
        }
        private void AddInterestRate(decimal sum)
        {
            decimal addition = sum * InterestRate / 100;
            OnUpdate($"Balance is negative. Interest rate: {addition:C}");
            base.Withdraw(addition);
        }
    }
}
