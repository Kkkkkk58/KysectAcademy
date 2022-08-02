using System;
using System.Collections.Generic;
using LABA_5.API;
using LABA_5.API.Interfaces;

namespace LABA_5.Database.API.Interfaces
{
    public interface IStore
    {
        LABA_5.API.IStore GetStore(string name);
        List<LABA_5.API.IStore> GetStores();
        Answer<bool> AddItems(int storeId, List<ICheckItem> storeItems);
        Answer<bool> RemoveItems(int storeId, List<ICheckItem> storeItems);
        Answer<LABA_5.API.IStore> CreateStore(string name);
        Answer<bool> ChangeItemPrice(int storeId, int itemId, decimal price);
        Answer<IDbItem> CreateItem(string itemName);
        List<IDbItem> GetItems();
        List<LABA_5.Database.Models.StoreItems> GetStoreItems();
        void ClearDB();
    }
}
