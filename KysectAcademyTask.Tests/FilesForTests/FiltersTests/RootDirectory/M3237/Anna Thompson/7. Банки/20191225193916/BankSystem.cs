using LABA_7.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LABA_7.API.Models
{
    public class BankSystem : IBankSystem
    {
        public BankSystem()
        {
            Banks = new List<IBank>();
        }

        private List<IBank> Banks { get; set; }
        private int NewBankId => (Banks.Count > 0) ? Banks.Max(x => x.Id) + 1 : 1;

        public void ConfirmTransaction(ITransaction Transaction)
        {
            var bank = Banks.FirstOrDefault(x => x.Id == Transaction.BankSenderId);
            bank.TransactionConformation(Transaction.Id);
        }

        public void CreateBank(decimal DebitAccountInterestOnBalance, decimal CreditAccountCreditLimit, decimal DoubtfulMaxSum, decimal CreditAccountComission)
        {
            var bank = new Bank(NewBankId, DebitAccountInterestOnBalance, CreditAccountCreditLimit, DoubtfulMaxSum, CreditAccountComission, this);
            Banks.Add(bank);
        }

        public IClient CreateClient(int BankId, string FirstName, string LastName, string IdDocument = null, string Adress = null)
        {
            var bank = Banks.FirstOrDefault(x => x.Id == BankId);
            return bank.CreateClient(FirstName, LastName, IdDocument, Adress);
        }

        public int GetBankIdOwnerAccountId(string AccountId)
        {
            foreach(var bank in Banks)
            {
                if (bank.CheckAccountAvailaility(AccountId))
                    return bank.Id;
            }
            return 0;
        }

        public List<int> GetBanksIds()
        {
            return Banks.Select(x => x.Id).ToList();
        }


        public void OnConfirmedTransaction(int BankSenderId, int BankRecipientId, string AccountSenderId, string AccountRecipientId, string TransactionId, TransactionType Type, decimal Sum)
        {
            var bank = Banks.FirstOrDefault(x => x.Id == BankRecipientId);
            bank.AddTransactionFromAnotherBank(BankSenderId, AccountSenderId, AccountRecipientId, TransactionId, Type, Sum);
        }
    }
}
