using System;
using System.Collections.Generic;

namespace LABA_5.API.Interfaces
{
    public interface IMarket
    {
        Answer<bool> CreateStore(string nameStore);
        Answer<bool> CreateItem(string itemName);
        string GetStoreWithSmallestPrice(string nameItem);
        string GetStoreWithSmallestSum(Dictionary<string, int> items);
        List<string> GetStores();
        List<string> GetItems();
        void AddItemsInStore(string nameStore, Dictionary<string, int> items);
        Answer<bool> BuyItemsInStore(string nameStore, Dictionary<string, int> items);
        void ChangeItemPriceInStore(string nameStore, string nameItem, decimal price);
    }
}
