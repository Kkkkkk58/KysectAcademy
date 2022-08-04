using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            Date date = new Date(21, 12, 2019);
            List<DYP> depositspercents = new List<DYP>();
            depositspercents.Add(new DYP(0, 5));
            depositspercents.Add(new DYP(50000, 5.5));
            depositspercents.Add(new DYP(100000, 6));

            Bank BNK = new Bank(0, new BankInformation(100000, 3.65, -100000, 500, depositspercents));

            BNK.NewClient("Sam", "Hyde", "US Marine str.", "1000000");
            BNK.NewClient("Adam", "Fried", "Brooklyn", "");

            BNK.ShowClients();

            BNK.NewCreditAccount("0", 1000000, date);
            BNK.NewDebitAccount("0", 1000000, date);
            BNK.NewDeposit("0", 1000000, date, new Date(21, 01, 2020));

            BNK.NewCreditAccount("1", 0, date);
            BNK.NewDebitAccount("1", 25000, date);
            BNK.NewDeposit("1", 45000, date, new Date(21, 02, 2020));
            BNK.NewDeposit("1", 45000, date, new Date(22, 12, 2019));

            BNK.ShowAccounts();

            BNK.Withdraw(5000, "3", date);
            Console.WriteLine("Withdrawal of 5000$ from ID3");

            for(int i = 0; i < 6; i++)
            {
                date.AddOneDay();
                Console.Write("Iterating: ");
                date.Show();
                BNK.DailyAccountReNewal(date);
            }

            BNK.Replenish(5000, "5");
            Console.WriteLine("Replenishment of 5000$ to ID5");

            for (int i = 0; i < 30; i++)
            {
                date.AddOneDay();
                Console.Write("Iterating: ");
                date.Show();
                BNK.DailyAccountReNewal(date);
            }

            BNK.ShowAccounts();

            BNK.Transfer(200000, "0", "5", date);
            Console.WriteLine("Transfer of 200000 from ID0 to ID5");

            BNK.ShowTransactions();
            BNK.ShowAccounts();


            BNK.Cancel("2");
            Console.WriteLine("Cancelation of transaction ID2");

            BNK.ShowAccounts();
            BNK.ShowTransactions();

            Console.ReadKey();
        }
    }
}
