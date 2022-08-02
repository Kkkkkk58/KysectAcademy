using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_7.API.Interfaces
{
    public interface ITransactionManager
    {
        ITransaction CreateTransaction(decimal Sum, IAccount Account, TransactionType Type, string AccountRecipientId = null);
    }
}
