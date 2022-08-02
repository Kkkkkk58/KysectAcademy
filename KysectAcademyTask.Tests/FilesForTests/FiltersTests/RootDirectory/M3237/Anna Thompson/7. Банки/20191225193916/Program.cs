using LABA_7.API.Models;
using System;

namespace LABA_7
{
    class Program
    {
        static void Main(string[] args)
        {
            Go();
            Console.ReadLine();
        }

        static void Go()
        {
            var s = new BankSystem();
            s.CreateBank(0.05M, 10000, 10000, 0.02M);
            s.CreateBank(0.10M, 10000, 10000, 0.06M);
            var client1 = s.CreateClient(1, "1", "11", "id1", "ad1");
            var client2 = s.CreateClient(2, "2", "22", "id2", "ad2");

            var a1 = client1.CreateDebitAccount();
            var a2 = client2.CreateDebitAccount();
            var t1 = a2.Replenish(10000);
            s.ConfirmTransaction(t1);
            var t2 = a2.Transfer(100, a1.Id);
            s.ConfirmTransaction(t2);

        }
    }
}
