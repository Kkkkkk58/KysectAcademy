using System;
using System.Collections.Generic;
using System.Text;

namespace Lab7
{
    public class Transaction
    {
        public Guid TransactionId { get; }
        public Account From { get; }
        public Account To { get; }
        public float Sum { get; }
        public Transaction(Account from, Account to, float sum)
        {
            From = from;
            To = to;
            Sum = sum;
            TransactionId = Guid.NewGuid();
        }
    }
}
