using LABA_7.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_7.API.Models
{
    public class Transaction : ITransaction
    {
        public string Id { get; set; }
        public int BankSenderId { get; set; }
        public string AccountSenderId { get; set; }
        public int BankRecipientId { get; set; }
        public string AccountRecipientId { get; set; }
        public TransactionStatus Status { get; set; }
        public TransactionType Type { get; set; }
        public decimal Sum { get; set; }
    }
}
