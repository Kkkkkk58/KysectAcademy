using System.Collections.Generic;
using LABA_5.API.Interfaces;

namespace LABA_5.API
{
    public interface IStore
    {
        string Name { get; set; }
        void AddItems(List<ICheckItem> storeItems);
        Answer<bool> ChangeItemPrice(int itemId, decimal price);
        Answer<decimal> GetItemPrice(int itemId);
        List<ICheckItem> GetAvailablePurchase(decimal sum);
        Answer<bool> Buy(List<ICheckItem> storeItems);
        Answer<decimal> GetSum(List<ICheckItem> items);
    }
}