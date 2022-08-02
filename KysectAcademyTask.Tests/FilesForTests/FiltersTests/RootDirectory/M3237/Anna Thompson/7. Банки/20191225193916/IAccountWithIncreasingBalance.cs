using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_7.API.Interfaces
{
    public interface IAccountWithIncreasingBalance
    {
        // Накопленные начисления 
        decimal UnaddressedBalance { get; set; }
        // Последнее начисление месячных процентов
        int TimestampLastMonthInterestAccrual { get; set; }
    }
}
