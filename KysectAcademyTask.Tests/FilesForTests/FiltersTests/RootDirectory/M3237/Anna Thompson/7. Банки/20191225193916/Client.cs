using LABA_7.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_7.API.Models
{
    public class Client : IClient
    {
        public Client(IBank Bank, string FirstName, string LastName, string IdDocument = null, string Adress = null)
        {
            this.Bank = Bank;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.IdDocument = IdDocument;
            this.Adress = Adress;
            CreditAccounts = new List<ICreditAccount>();
            DebitAccounts = new List<IDebitAccount>();
            DepositAccounts = new List<IDepositAccout>();
        }

        public IBank Bank { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdDocument { get; set; }
        public string Adress { get; set; }
        public bool Doubtful => string.IsNullOrEmpty(IdDocument) || string.IsNullOrEmpty(Adress);

        public List<ICreditAccount> CreditAccounts { get; set; }
        public List<IDebitAccount> DebitAccounts { get; set; }
        public List<IDepositAccout> DepositAccounts { get; set; }

        public ICreditAccount CreateCreditAccount()
        {
            return Bank.CreateCreditAccount(this);
        }

        public IDebitAccount CreateDebitAccount()
        {
            return Bank.CreateDebitAccount(this);
        }

        public IDepositAccout CreateDepositAccount(decimal Sum)
        {
            return Bank.CreateDepositAccount(this, Sum);
        }
    }
}
