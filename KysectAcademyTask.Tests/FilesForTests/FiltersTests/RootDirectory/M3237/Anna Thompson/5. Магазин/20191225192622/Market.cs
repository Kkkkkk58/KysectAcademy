using System;
using System.Collections.Generic;
using LABA_5.API.Interfaces;
using System.Linq;

namespace LABA_5.API.Models
{
    public class Market : IMarket
    {
        private List<IStore> _stores { get; set; }
        private List<IDbItem> _items { get; set; }

        private LABA_5.Database.API.Interfaces.IStore dbStore { get; set; }

        public Market(LABA_5.Database.API.Interfaces.IStore DbStore)
        {
            dbStore = DbStore;

            _stores = dbStore.GetStores();
            _items = dbStore.GetItems();
        }

        public Answer<bool> CreateStore(string nameStore)
        {
            var answer = new Answer<bool>();

            var response = dbStore.CreateStore(nameStore);

            if (response.Success)
            {
                answer.Data = true;
                _stores.Add(response.Data);
            }
            else
            {
                answer.Error = response.Error;
            }

            return answer;
        }

        public Answer<bool> CreateItem(string itemName)
        {
            var answer = new Answer<bool>();
            var response = dbStore.CreateItem(itemName);

            if (response.Success)
            {
                answer.Data = true;
                _items.Add(response.Data);
            }
            else
            {
                answer.Error = response.Error;
            }

            return answer;
        }

        public IStore GetStore(string nameStore)
        {
            return _stores.FirstOrDefault(x => x.Name == nameStore);
        }

        public string GetStoreWithSmallestPrice(string nameItem)
        {
            IStore answer = null;
            var item = _items.FirstOrDefault(x => x.Name == nameItem);
            if (item != null)
            {          
                decimal min = decimal.MaxValue;

                foreach (var store in _stores)
                {
                    var response = store.GetItemPrice(item.ItemId);
                    if (response.Success && response.Data < min)
                    {
                        min = response.Data;
                        answer = store;
                    }
                }
            }

            return (answer != null) ? answer.Name : null;
        }

        public string GetStoreWithSmallestSum(Dictionary<string, int> items)
        {
            IStore answer = null;

            var needItems = new List<ICheckItem>();
            foreach(var i in items)
            {
                var item = _items.FirstOrDefault(x => x.Name == i.Key);
                if (item == null) return null;
                needItems.Add(new CheckItem(item.ItemId, i.Value));
            }

            decimal min = decimal.MaxValue;

            foreach (var store in _stores)
            {
                var response = store.GetSum(needItems);
                if (response.Success && response.Data < min)
                {
                    min = response.Data;
                    answer = store;
                }
            }

            return (answer != null) ? answer.Name : null;
        }

        public List<string> GetStores()
        {
            return _stores.Select(x => x.Name).ToList();
        }

        public List<string> GetItems()
        {
            return _items.Select(x => x.Name).ToList();
        }

        public void AddItemsInStore(string nameStore, Dictionary<string, int> items)
        {
            var store = _stores.FirstOrDefault(x => x.Name == nameStore);
            store.AddItems(items.Select(x => (ICheckItem)(new CheckItem(_items.FirstOrDefault(g => g.Name == x.Key).ItemId, x.Value))).ToList());
        }

        public Answer<bool> BuyItemsInStore(string nameStore, Dictionary<string, int> items)
        {
            var store = _stores.FirstOrDefault(x => x.Name == nameStore);
            return store.Buy(items.Select(x => (ICheckItem)(new CheckItem(_items.FirstOrDefault(g => g.Name == x.Key).ItemId, x.Value))).ToList());
        }

        public void ChangeItemPriceInStore(string nameStore, string nameItem, decimal price)
        {
            var store = _stores.FirstOrDefault(x => x.Name == nameStore);
            var item = _items.FirstOrDefault(x => x.Name == nameItem);
            store.ChangeItemPrice(item.ItemId, price);
        }
    }
}
