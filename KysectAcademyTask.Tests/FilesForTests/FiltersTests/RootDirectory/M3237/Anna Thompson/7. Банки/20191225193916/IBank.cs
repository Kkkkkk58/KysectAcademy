using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_7.API.Interfaces
{
    public interface IBank : ITransactionManager
    {
        int Id { get; set; }
        decimal DebitAccountInterestOnBalance { get; set; }
        decimal CreditAccountInterestOnBalance { get; set; }
        decimal CreditAccountCreditLimit { get; set; }
        decimal CreditAccountComission { get; set; }
        decimal DoubtfulMaxSum { get; set; }
        void TransactionCancellation(string TransactionId);
        void TransactionConformation(string TransactionId);
        void AddTransactionFromAnotherBank(int BankSenderId, string AccountSenderId, string AccountRecipientId, string TransactionId, TransactionType Type, decimal Sum);
        IClient CreateClient(string FirstName, string LastName, string IdDocument = null, string Adress = null);
        ICreditAccount CreateCreditAccount(IClient Client);
        IDebitAccount CreateDebitAccount(IClient Client);
        IDepositAccout CreateDepositAccount(IClient Client, decimal Sum);
        bool CheckAccountAvailaility(string AccountId);
    }
}
