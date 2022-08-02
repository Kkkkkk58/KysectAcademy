using LABA_7.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

namespace LABA_7.API.Models
{
    public class Bank : IBank
    {
        public Bank(int Id, decimal DebitAccountInterestOnBalance, decimal CreditAccountCreditLimit, decimal DoubtfulMaxSum, decimal CreditAccountComission, IBankSystem BankSystem)
        {
            this.Id = Id;
            this.DebitAccountInterestOnBalance = DebitAccountInterestOnBalance;
            this.CreditAccountInterestOnBalance = CreditAccountInterestOnBalance;
            this.CreditAccountCreditLimit = CreditAccountCreditLimit;
            this.DoubtfulMaxSum = DoubtfulMaxSum;
            this.CreditAccountComission = CreditAccountComission;
            Transactions = new List<ITransaction>();
            Clients = new List<IClient>();
            Accounts = new List<IAccount>();
            this.BankSystem = BankSystem;

            var _recalculation = new Thread(Recalculation);
            _recalculation.Start();
        }

        public int Id { get; set; }
        public decimal DebitAccountInterestOnBalance { get; set; }
        public decimal CreditAccountInterestOnBalance { get; set; }
        public decimal CreditAccountCreditLimit { get; set; }
        public decimal DoubtfulMaxSum { get; set; }
        public decimal CreditAccountComission { get; set; }
        private List<ITransaction> Transactions { get; set; }
        private List<IClient> Clients { get; set; }
        private List<IAccount> Accounts { get; set; }
        private IBankSystem BankSystem { get; set; }
        private int MaxAccountId { get; set; }
        private string NewAccountId => $"{this.Id}_{MaxAccountId + 1}";

        public void AddTransactionFromAnotherBank(int BankSenderId, string AccountSenderId, string AccountRecipientId, string TransactionId, TransactionType Type, decimal Sum)
        {
            var transaction = new Transaction()
            {
                BankSenderId = BankSenderId,
                AccountSenderId = AccountSenderId,
                BankRecipientId = this.Id,
                AccountRecipientId = AccountRecipientId,
                Status = TransactionStatus.Confirmed,
                Type = Type,
                Sum = Sum,
                Id = $"{this.Id}_{this.Transactions.Max(x => x.Id) + 1}"
            };
            var account = Accounts.FirstOrDefault(x => x.Id == transaction.AccountRecipientId);
            switch (transaction.Type)
            {
                case TransactionType.Transfer: account.Balance += transaction.Sum; break;
            }
        }

        public bool CheckAccountAvailaility(string AccountId)
        {
            return Accounts.FirstOrDefault(x => x.Id == AccountId) != null;
        }

        public IClient CreateClient(string FirstName, string LastName, string IdDocument = null, string Adress = null)
        {
            var client = new Client(this, FirstName, LastName, IdDocument, Adress);
            Clients.Add(client);
            return client;
        }

        public ICreditAccount CreateCreditAccount(IClient Client)
        {
            var account = new CreditAccount(Client, this, NewAccountId, this.CreditAccountInterestOnBalance, DoubtfulMaxSum, CreditAccountCreditLimit, CreditAccountComission);
            Accounts.Add(account);
            MaxAccountId++;
            return account;
        }

        public IDebitAccount CreateDebitAccount(IClient Client)
        {
            var account = new DebitAccount(Client, this, NewAccountId, DebitAccountInterestOnBalance, DoubtfulMaxSum);
            Accounts.Add(account);
            MaxAccountId++;
            return account;
        }

        public IDepositAccout CreateDepositAccount(IClient Client, decimal Sum)
        {
            decimal procent = 0.05M;
            if (Sum > 50000 && Sum <= 100000) procent = 0.055M;
            if (Sum > 100000) procent = 0.06M;
            var account = new DepositAccount(Client, this, NewAccountId, procent, DoubtfulMaxSum, (int)(DateTime.UtcNow.AddYears(1) - new DateTime(1970, 1, 1)).TotalSeconds);
            Accounts.Add(account);
            MaxAccountId++;
            return account;
        }

        public ITransaction CreateTransaction(decimal Sum, IAccount Account, TransactionType Type, string AccountRecipientId = null)
        {
            var transaction = new Transaction()
            {
                BankSenderId = this.Id,
                AccountSenderId = Account.Id,
                BankRecipientId = (AccountRecipientId == null) ? this.Id : (int)BankSystem.GetBankIdOwnerAccountId(AccountRecipientId),
                AccountRecipientId = (AccountRecipientId == null) ? Account.Id : AccountRecipientId,
                Status = TransactionStatus.Wait,
                Type = Type,
                Sum = Sum,
                Id = $"{this.Id}_{this.Transactions.Max(x => x.Id) + 1}"
            };
            Transactions.Add(transaction);
            Logger.Info($"New Transaction: " +
                $"BankSenderId - {transaction.BankSenderId}; " +
                $"AccountSenderId - {transaction.AccountSenderId}; " +
                $"BankRecipientId - {transaction.BankRecipientId}; " +
                $"Status - {transaction.Status}; " +
                $"Type - {transaction.Type}; " +
                $"Sum - {transaction.Sum}; " +
                $"Id - {transaction.Id}");
            return transaction;
        }

        public void TransactionCancellation(string TransactionId)
        {
            this.Transactions.FirstOrDefault(x => x.Id == TransactionId).Status = TransactionStatus.Canceled;
            Logger.Info($"TransactionCancellation: Id - {TransactionId}");
        }

        public void TransactionConformation(string TransactionId)
        {
            var transaction = this.Transactions.FirstOrDefault(x => x.Id == TransactionId);
            transaction.Status = TransactionStatus.Confirmed;
            var account = Accounts.FirstOrDefault(x => x.Id == transaction.AccountSenderId);
            switch (transaction.Type)
            {
                case TransactionType.Replenish: account.Balance += transaction.Sum; break;
                case TransactionType.Transfer: account.Balance -= transaction.Sum; break;
                case TransactionType.Withdrawal: account.Balance -= transaction.Sum; break;
            }
            if (this.Id != transaction.BankRecipientId)
                BankSystem.OnConfirmedTransaction(this.Id, transaction.BankRecipientId, transaction.AccountSenderId, transaction.AccountRecipientId, transaction.Id, transaction.Type, transaction.Sum);

            Logger.Info($"TransactionConformation: Id - {TransactionId}");
        }
    
        private void Recalculation()
        {
            foreach (var account in Accounts)
                account.Recalculation();
            Thread.Sleep(24 * 60 * 60 * 1000);
            Recalculation();
        }
    }
}
