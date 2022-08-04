using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    class Deposit : Account
    {
        Date WithdrawalLimitedDue;
        Date LastMonthlyPercentCharge;
        Date LastDailyPercentCharge;
        double YearlyPercent;
        double PercentPay;


        public Deposit(string clientid, double money, Date today, bool trust, BankInformation bankinfo, Date wlimit)
        {
            Type = "Deposit";
            ID = (bankinfo.AvailibleAccountID).ToString();
            ClientID = clientid;
            Money = money;
            Trustful = trust;
            WithdrawalLimitedDue = new Date(wlimit);
            LastMonthlyPercentCharge = new Date(today); 
            LastDailyPercentCharge = new Date(today);
            TrustLimit = bankinfo.TrustLimit;
            PercentPay = 0;

            int i = 0;
            while(i < bankinfo.DepositYearlyPercent.Count)
            {
                if (bankinfo.DepositYearlyPercent[i].NeededAmount <= money)
                    YearlyPercent = bankinfo.DepositYearlyPercent[i].AppropriatePercent;
                else
                    break;
                i++;
            }
        }
        public override void Replenish(double Amount)
        {
            Money += Amount;
        }
        public override bool Withdraw(double Amount, Date Today)
        {
            if (WithdrawalLimitedDue > Today || Money < Amount 
                || (!Trustful && Amount > TrustLimit))
                return false;
            else
            {
                Money -= Amount;
                return true;
            }
        }
        public override void ReNew(Date today)
        {
            if (today.year > LastMonthlyPercentCharge.year ||
                (today.year == LastMonthlyPercentCharge.year && today.month > LastMonthlyPercentCharge.month))
            {
                LastMonthlyPercentCharge = new Date(today);
                Replenish(PercentPay);
                PercentPay = 0;
            }
            if(!today.Equals(LastDailyPercentCharge))
            {
                PercentPay += (YearlyPercent / 36500) * Money;
                LastDailyPercentCharge = new Date(today);
            }
        }
    }
}
