using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_7.API.Interfaces
{
    public interface IBankSystem
    {
        void OnConfirmedTransaction(int BankSenderId, int BankRecipientId, string AccountSenderId, string AccountRecipientId, string TransactionId, TransactionType Type, decimal Sum);
        int GetBankIdOwnerAccountId(string AccountId);
        void CreateBank(decimal DebitAccountInterestOnBalance, decimal CreditAccountCreditLimit, decimal DoubtfulMaxSum, decimal CreditAccountComission);
        List<int> GetBanksIds();
        IClient CreateClient(int BankId, string FirstName, string LastName, string IdDocument = null, string Adress = null);
        void ConfirmTransaction(ITransaction Transaction);
    }
}
