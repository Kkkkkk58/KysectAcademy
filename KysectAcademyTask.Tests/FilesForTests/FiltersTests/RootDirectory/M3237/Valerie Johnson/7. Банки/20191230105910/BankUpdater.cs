using System;
using System.Collections.Generic;
using System.Text;

namespace Lab7
{
    public class BankUpdater
    {
        private static List<Bank> banks = new List<Bank>();
        public int Day { get; private set; } = 1;
        public void NextDay()
        {
            Day += 1;
            if (Day == 30)
            {
                NextMonth();
            }
            foreach (var bank in banks)
            {
                bank.UpdateDay();
            }
        }
        public void NextMonth()
        {
            Day = 1;
            foreach (var bank in banks)
            {
                bank.UpdateMonth();
            }
        }
        public void AddSubscriber(Bank bank)
        {
            banks.Add(bank);
        }
    }
}
