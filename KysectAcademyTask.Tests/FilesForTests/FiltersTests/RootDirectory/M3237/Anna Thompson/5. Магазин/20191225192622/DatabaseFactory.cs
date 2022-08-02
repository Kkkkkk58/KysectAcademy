using System;
using LABA_5.Database.API.Interfaces;
using LABA_5.Database.Modul.Store;

namespace LABA_5.Database.API
{
    public static class DatabaseFactory
    {
        public static IStore Store { get; private set; }

        public static IStore GetStoreInstance()
        {
            if (Store == null)
                Store = new DatabaseStore();
            return Store;
        }
    }
}
