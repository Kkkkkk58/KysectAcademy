using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    class Transaction
    {
        public string ID;
        public string Type; // "RNT" - Replenishment "WWL" - Withdrawal "TFR" - Transfer
        public string FirstAcc;
        public string SecAcc;
        public double Amount;
        public bool IsCanceled;

        public Transaction(string tp, string acc, double amount, BankInformation BI)
        {
            ID = BI.AvailibleTransactionID.ToString();
            Type = tp;
            Amount = amount;
            if (tp == "TFR")
                throw new Exception("Not enough parameters for transfer transaction");
            FirstAcc = acc;
            IsCanceled = false;
        }

        public Transaction(string tp, string facc, string sacc, double amount, BankInformation BI)
        {
            ID = BI.AvailibleTransactionID.ToString();
            Type = tp;
            Amount = amount;
            if (tp != "TFR")
                throw new Exception("Too many parameters");
            FirstAcc = facc;
            SecAcc = sacc;
            IsCanceled = false;
        }
    }
}
