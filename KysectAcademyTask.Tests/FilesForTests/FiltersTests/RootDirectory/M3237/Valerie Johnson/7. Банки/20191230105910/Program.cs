using System;

namespace Lab7
{
    class Program
    {
        static void Main()
        {
            Client c1 = new Client
            {
                FirstName = "aaa",
                LastName = "aaa"   
            };
            Client c2 = new Client
            {
                FirstName = "bbb",
                LastName = "bbb"
            };
            Bank bank = new Bank("aaa", 5, 150, 100, 1000);
            BankUpdater bankUpdater = new BankUpdater();
            bankUpdater.AddSubscriber(bank);
            Account a1 = bank.CreateAccount(c1, Type.AccountType.Debit, 300);
            Account a2 = bank.CreateAccount(c2, Type.AccountType.Debit, 500);
            Transaction t1 = bank.Transfer(a1, a2, 150);
            for (int i = 0; i <= 365; i++)
            {
                bank.UpdateDay();
            }
            bank.UpdateMonth();
            Console.WriteLine(a1.Balance);
        }
    }
}
