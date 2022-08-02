using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LABA_5.Converter
{
    public static class Converter
    {
        public static void ToFileStore(LABA_5.Database.API.Interfaces.IStore dbFile, LABA_5.Database.API.Interfaces.IStore db)
        {
            Convert(db, dbFile);
        } 

        public static void ToDatabaseStore(LABA_5.Database.API.Interfaces.IStore dbFile, LABA_5.Database.API.Interfaces.IStore db)
        {
            Convert(dbFile, db);
        }

        private static void Convert(LABA_5.Database.API.Interfaces.IStore parent, LABA_5.Database.API.Interfaces.IStore children)
        {
            children.ClearDB();

            foreach (var item in parent.GetItems())
            {
                children.CreateItem(item.Name);
            }

            foreach (var store in parent.GetStores())
            {
                children.CreateStore(store.Name);
            }

            foreach(var storeItem in parent.GetStoreItems())
            {
                children.AddItems(storeItem.StoreId, new List<API.ICheckItem>() { new LABA_5.API.Models.CheckItem(storeItem.ItemId, storeItem.AvailableValue) });
            }
        }
    }
}
