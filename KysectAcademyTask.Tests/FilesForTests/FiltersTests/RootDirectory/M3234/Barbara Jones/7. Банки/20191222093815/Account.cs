using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    abstract class Account
    {
        public string Type;
        public string ID;
        public string ClientID;
        public double Money;
        public bool Trustful;
        public double TrustLimit;

        public abstract void ReNew(Date today);
        public abstract void Replenish(double Amount);
        public abstract bool Withdraw(double Amount, Date today);
        public bool Withdraw(double Amount) //In Case of Cancelation
        {
            if (Money >= Amount)
            {
                Money -= Amount;
                return true;
            }
            else
                return false;
        }

    }
}
