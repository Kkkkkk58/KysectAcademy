using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_7.API.Interfaces
{
    public interface IDepositAccout : IAccount, IAccountWithIncreasingBalance
    {
        // Срок истечения карты
        int TimestampExpiration { get; set; }
    }
}
