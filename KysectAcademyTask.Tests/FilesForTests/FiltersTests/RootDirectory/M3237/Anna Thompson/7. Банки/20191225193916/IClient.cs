using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_7.API.Interfaces
{
    public interface IClient
    {
        IBank Bank { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string IdDocument { get; set; }
        string Adress { get; set; }
        bool Doubtful { get; }
        List<ICreditAccount> CreditAccounts { get; set; }
        List<IDebitAccount> DebitAccounts { get; set; }
        List<IDepositAccout> DepositAccounts { get; set; }
        ICreditAccount CreateCreditAccount();
        IDebitAccount CreateDebitAccount();
        IDepositAccout CreateDepositAccount(decimal Sum);
    }
}
