using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    class DebitAccount : Account
    {
        Date LastMonthlyPercentCharge;
        Date LastDailyPercentCharge;
        double YearlyPercent;
        double PercentPay;

        public DebitAccount(string clientid, double money, Date today, bool trust, BankInformation bankinfo)
        {
            Type = "Debit";
            ID = (bankinfo.AvailibleAccountID).ToString();
            ClientID = clientid;
            PercentPay = 0; 
            Money = money;
            Trustful = trust;
            LastMonthlyPercentCharge = new Date(today);
            LastDailyPercentCharge = new Date(today);
            YearlyPercent = bankinfo.DebitYearlyPercent;
            TrustLimit = bankinfo.TrustLimit;
        }
        public override void Replenish(double Amount)
        {
            Money += Amount;
        }
        public override bool Withdraw(double Amount, Date today)
        {
            if (Money < Amount && (Trustful || Amount <= TrustLimit))
            {
                Money -= Amount;
                return true;
            }
            else
                return false;
        }
        public override void ReNew(Date today)
        {
            //Console.Write(ID + " " + today.day + "." + today.month + "." + today.year + "<" + LastMonthlyPercentCharge.day + "." + LastMonthlyPercentCharge.month + "." + LastMonthlyPercentCharge.year + ";");
            if (today.year > LastMonthlyPercentCharge.year ||
                (today.year == LastMonthlyPercentCharge.year && today.month > LastMonthlyPercentCharge.month))
            {
                //Console.Write("CHARGING " + ID + ";");
                LastMonthlyPercentCharge = new Date(today);
                Replenish(PercentPay);
                PercentPay = 0;
            }
            if (!today.Equals(LastDailyPercentCharge))
            {
                PercentPay += (YearlyPercent / 36500) * Money;
                LastDailyPercentCharge = new Date(today);
            }
        }
    }
}
