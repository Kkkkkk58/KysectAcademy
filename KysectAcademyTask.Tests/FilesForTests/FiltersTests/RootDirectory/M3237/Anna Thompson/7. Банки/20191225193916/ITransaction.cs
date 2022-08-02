using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_7.API.Interfaces
{
    public interface ITransaction
    {
        string Id { get; set; }
        int BankSenderId { get; set; }
        string AccountSenderId { get; set; }
        int BankRecipientId { get; set; }
        string AccountRecipientId { get; set; }
        TransactionStatus Status { get; set; }
        TransactionType Type { get; set; }
        decimal Sum { get; set; }
    }
}
