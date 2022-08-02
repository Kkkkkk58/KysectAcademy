using OOPLab7.Accounts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace OOPLab7
{
    class Transaction : IComparable<Transaction>
    {
        public Transaction(Account from, Account to, decimal sum)
        {
            From = from;
            To = to;
            Sum = sum;
            TransactionId = Guid.NewGuid();
        }
        public Guid TransactionId { get; }
        public Account From { get; }
        public Account To { get; }
        public decimal Sum { get; }

        public int CompareTo([AllowNull] Transaction other)
        {
            return TransactionId.CompareTo(other.TransactionId);
        }
    }
}
