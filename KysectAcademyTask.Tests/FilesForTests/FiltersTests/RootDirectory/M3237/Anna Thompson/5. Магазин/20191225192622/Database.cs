using System;
namespace LABA_5.Database.API
{
    public abstract class Database
    {
        protected readonly object DbLock = new object();

        public Database()
        {

        }
    }
}
