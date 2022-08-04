using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    class CreditAccount : Account
    {
        double CreditLimit;
        Date LastComissionWithdrawal;
        double ComissionWithdrawal;
        bool AccountWasNegative;

        public CreditAccount(string clientid, double money, Date today, bool trust, BankInformation bankinfo)
        {
            Type = "Credit";
            ID = (bankinfo.AvailibleAccountID).ToString();
            ClientID = clientid;
            Money = money;
            Trustful = trust;
            TrustLimit = bankinfo.TrustLimit;
            CreditLimit = bankinfo.CreditLimit;
            ComissionWithdrawal = bankinfo.CreditMonthlyComission;
            LastComissionWithdrawal = new Date(today);
            AccountWasNegative = false;
        }
        public override void Replenish(double Amount)
        {
            Money += Amount;
        }
        public override bool Withdraw(double Amount, Date Today)
        {
            if (Money - Amount < CreditLimit 
                || (!Trustful && Amount > TrustLimit))
                return false;
            else
            {
                Money -= Amount;
                if (Money < 0)
                    AccountWasNegative = true;

                return true;
            }
        }
        public override void ReNew(Date today)
        {
            if (today.year > LastComissionWithdrawal.year ||
                (today.year == LastComissionWithdrawal.year && today.month > LastComissionWithdrawal.month))
            {
                if(AccountWasNegative)
                    Money -= ComissionWithdrawal;

                AccountWasNegative = false;
                LastComissionWithdrawal = new Date(today);
            }
            if (Money < 0)
                AccountWasNegative = true;
        }
    }
}
