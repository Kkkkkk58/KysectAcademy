using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    class Bank
    {
        public int ID;
        public List<Account> accounts;
        public List<Client> clients;
        public List<Transaction> transactions;
        public BankInformation BankInfo;
        
        public Bank(int id, BankInformation bankinfo)
        {
            ID = id;
            accounts = new List<Account>();
            clients = new List<Client>();
            transactions = new List<Transaction>();
            BankInfo = bankinfo;
        }

        public void NewDebitAccount(string clientid, double money, Date today)
        {
            int CL = FindClient(clientid);
            accounts.Add(new DebitAccount(clientid, money, today, clients[CL].istrustful(), BankInfo));
            BankInfo.AvailibleAccountID += 1;
        }
        public void NewDeposit(string clientid, double money, Date today, Date limitdue)
        {
            int CL = FindClient(clientid);
            accounts.Add(new Deposit(clientid, money, today, clients[CL].istrustful(), BankInfo, limitdue));
            BankInfo.AvailibleAccountID += 1;
        }
        public void NewCreditAccount(string clientid, double money, Date today)
        {
            int CL = FindClient(clientid);
            accounts.Add(new CreditAccount(clientid, money, today, clients[CL].istrustful(), BankInfo));
            BankInfo.AvailibleAccountID += 1;
        }
        public void NewClient(string NM, string SNM, string ADD, string PSS)
        {
            clients.Add(new Client(NM, SNM, ADD, PSS, BankInfo));
            BankInfo.AvailibleClientID += 1;
        }

        public void DailyAccountReNewal(Date today)
        {
            foreach (Account account in accounts)
                account.ReNew(today);
        }
        public void Cancel(string ID)
        {
            int i = FindTransaction(ID);
            if (transactions[i].IsCanceled)
                throw new Exception("Transaction was already canceled");

            if (transactions[i].Type == "WWL")
            {
                int FRM = FindAccount(transactions[i].FirstAcc);
                accounts[FRM].Replenish(transactions[i].Amount);
                transactions[i].IsCanceled = true;
            }

            if(transactions[i].Type == "RNT")
            {
                int TO = FindAccount(transactions[i].FirstAcc);
                bool t = accounts[TO].Withdraw(transactions[i].Amount);
                if (t)
                    transactions[i].IsCanceled = true;
                else
                    throw new Exception("Transaction cannot be canceled");
            }

            if (transactions[i].Type == "TFR")
            {
                int FRM = FindAccount(transactions[i].FirstAcc);
                int TO = FindAccount(transactions[i].SecAcc);
                bool t = accounts[TO].Withdraw(transactions[i].Amount);
                if (t)
                {
                    accounts[FRM].Replenish(transactions[i].Amount);
                    transactions[i].IsCanceled = true;
                }
                else
                    throw new Exception("Transaction cannot be canceled");
            }
        }
        public void Replenish(double Amount, string ToID)
        {
            int TO = FindAccount(ToID);
            accounts[TO].Replenish(Amount);
            transactions.Add(new Transaction("RNT", ToID, Amount, BankInfo));
            BankInfo.AvailibleTransactionID += 1;
        }
        public void Withdraw(double Amount, string FromID, Date today)
        {
            int FRM = FindAccount(FromID);
            bool t = accounts[FRM].Withdraw(Amount, today);
            if (!t)
                throw new Exception("Withdrawal cannot be completed");
            transactions.Add(new Transaction("WWL", FromID, Amount, BankInfo));
            BankInfo.AvailibleTransactionID += 1;
        }
        public void Transfer(double Amount, string FromID, string ToID, Date today)
        {
            int FRM = FindAccount(FromID);
            int TO = FindAccount(ToID);
            bool t = accounts[FRM].Withdraw(Amount, today);
            if (t)
            {
                accounts[TO].Replenish(Amount);
                transactions.Add(new Transaction("TFR", FromID, ToID, Amount, BankInfo));
            }
            else
                throw new Exception("Transfer cannot be completed");
            BankInfo.AvailibleTransactionID += 1;
        }

        public int FindAccount(string ID)
        {
            int t = 0;
            int ans = -1;
            while (t < accounts.Count())
            {
                if (accounts[t].ID == ID)
                { ans = t; break; }
                t++;
            }

            if (ans == -1)
                throw new Exception("Requested ID does not exists");
            return ans;
        }
        public int FindClient(string ID)
        {
            int t = 0;
            int ans = -1;
            while (t < clients.Count())
            {
                if (clients[t].ID == ID)
                { ans = t; break; }
                t++;
            }

            if (ans == -1)
                throw new Exception("Requested ID does not exists");
            return ans;
        }
        public int FindTransaction(string ID)
        {
            int t = 0;
            int ans = -1;
            while (t < transactions.Count())
            {
                if (transactions[t].ID == ID)
                { ans = t; break; }
                t++;
            }

            if (ans == -1)
                throw new Exception("Requested ID does not exists");
            return ans;
        }

        public void ShowClients()
        {
            Console.WriteLine("-----Clientlist:-------");
            Console.WriteLine("ID\tName\tSurname\tTrusted\tPassprt\tAddress");
            foreach(Client client in clients)
                Console.WriteLine(client.ID + "\t" + client.Name + "\t" + client.Surname + "\t" + client.istrustful() + "\t" + client.Passport + "\t" + client.Address);
            Console.WriteLine("-----------------------");
        }
        public void ShowAccounts()
        {
            Console.WriteLine("-----Accountlist:------");
            Console.WriteLine("ID\tClID\tType\tMoney");
            foreach (Account account in accounts)
                Console.WriteLine(account.ID + "\t" + account.ClientID + "\t" + account.Type + "\t" + account.Money);
            Console.WriteLine("-----------------------");
        }
        public void ShowTransactions()
        {
            Console.WriteLine("-----Transactions:-----");
            Console.WriteLine("ID\tType\tID1\tID2\tCncld\tMoney");
            foreach (Transaction transaction in transactions)
                Console.WriteLine(transaction.ID + "\t" + transaction.Type + "\t" + transaction.FirstAcc + "\t" + transaction.SecAcc + "\t" + transaction.IsCanceled + "\t" + transaction.Amount);
            Console.WriteLine("-----------------------");
        }
    }

    public class BankInformation
    {
        public int AvailibleClientID;
        public int AvailibleAccountID;
        public int AvailibleTransactionID;
        public double TrustLimit;
        public double DebitYearlyPercent;
        public double CreditLimit;
        public double CreditMonthlyComission;
        public List<DYP> DepositYearlyPercent;

        public BankInformation(double trustlimit, double debityearlypercent, 
            double creditlimit, double creditmonthlycomission, List<DYP> deposityearlypercent)
        {
            AvailibleClientID = 0;
            AvailibleAccountID = 0;
            AvailibleTransactionID = 0;
            TrustLimit = trustlimit;
            DebitYearlyPercent = debityearlypercent;
            CreditLimit = creditlimit;
            CreditMonthlyComission = creditmonthlycomission;
            DepositYearlyPercent = deposityearlypercent;
        }
    }

    public class DYP
    {
        public double NeededAmount;
        public double AppropriatePercent;

        public DYP(double neededamount, double appropriatepercent)
        {
            NeededAmount = neededamount;
            AppropriatePercent = appropriatepercent;
        }
    }
}
