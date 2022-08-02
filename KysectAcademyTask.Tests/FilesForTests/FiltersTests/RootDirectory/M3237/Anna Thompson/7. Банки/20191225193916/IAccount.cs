using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_7.API.Interfaces
{
    public interface IAccount
    {
        string Id { get; set; }
        int BankId { get; set; }
        decimal Balance { get; set; }
        ITransaction Withdrawal(decimal Sum);
        ITransaction Replenish(decimal Sum);
        ITransaction Transfer(decimal Sum, string AccountId);
        IClient Client { get; set; }
        void Recalculation();
        // Последнее начисление процентов
        int TimestampLastInterestAccrual { get; set; }
        // Проценты для начисления (год)
        decimal InterestOnBalance { get; set; }
    }
}
