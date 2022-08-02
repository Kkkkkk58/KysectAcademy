using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_7.API.Interfaces
{
    public interface ICreditAccount : IAccount
    {
        decimal CreditLimit { get; set; }
        decimal CreditAccountComission { get; set; }
    }
}
