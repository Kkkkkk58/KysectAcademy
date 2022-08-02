using LABA_5.API;
using LABA_5.API.Interfaces;
using LABA_5.Database.API.Interfaces;
using LABA_5.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LABA_5.API.Models;

namespace LABA_5.FileStore
{
    public class FileDatabase : Database.API.Interfaces.IStore
    {
        private Models.Stores Stores { get; set; }
        private Models.StoreItems StoreItems { get; set; }
        private Models.Items Items { get; set; }
        public FileDatabase()
        {
            Stores = new Models.Stores();
            Items = new Models.Items();
            StoreItems = new Models.StoreItems(Items);
        }

        public Answer<bool> AddItems(int storeId, List<ICheckItem> storeItems)
        {
            var answer = new Answer<bool>();

            var store = Stores[storeId];
            if (store == null)
            {
                answer.Error = $"Not found store: {storeId}";
                return answer;
            }

            foreach (var newItem in storeItems)
            {
                var item = StoreItems[store.Id].FirstOrDefault(x => x.ItemId == newItem.ItemId);
                if (item == null)
                {
                    StoreItems.Add(new LABA_5.Database.Models.StoreItems()
                    {
                        StoreId = store.Id,
                        ItemId = newItem.ItemId,
                        Price = 0,
                        AvailableValue = newItem.Value
                    });
                }
                else
                {
                    item.AvailableValue += newItem.Value;
                }

            }
            StoreItems.SaveChanges();
            answer.Data = true;
            return answer;
        }

        public Answer<bool> RemoveItems(int storeId, List<ICheckItem> storeItems)
        {
            var answer = new Answer<bool>();

            var store = Stores[storeId];
            if (store == null)
            {
                answer.Error = $"Not found store: {storeId}";
                return answer;
            }

            foreach (var item in storeItems)
            {
                var storeItem = StoreItems[store.Id].FirstOrDefault(x => x.ItemId == item.ItemId);
                if (storeItem == null || storeItem.AvailableValue < item.Value)
                {
                    answer.Error = $"Not enough goods in stock: {item.ItemId} in store {storeId}";
                    return answer;
                }
                storeItem.AvailableValue -= item.Value;
            }
            StoreItems.SaveChanges();
            answer.Data = true;
            return answer;
        }

        public Answer<API.IStore> CreateStore(string name)
        {
            var answer = new Answer<API.IStore>();

            var store = Stores[name];
            if (store != null)
            {
                answer.Error = "Store with the same name already exists";
            }
            else
            {
                Stores.Add(new LABA_5.Database.Models.Stores()
                {
                    Name = name
                });
                Stores.SaveChanges();

                answer.Data = GetStore(name);
            }

            return answer;
        }

        public API.IStore GetStore(string name)
        {
            var store = Stores[name];
            if (store == null) return null;
            var items = StoreItems[store.Id];
            List<IStoreItem> storeItems = new List<LABA_5.API.Interfaces.IStoreItem>();
            items.ForEach(x =>
            {
                storeItems.Add(new StoreItem(x.ItemId, x.Price, x.AvailableValue));
            });
            return new LABA_5.API.Store(name, store.Id, storeItems, this);
        }

        public List<API.IStore> GetStores()
        {
            var answer = new List<API.IStore>();
            foreach (var name in Stores.Select(x => x.Name))
            {
                answer.Add(GetStore(name));
            }
            return answer;
        }

        public Answer<bool> ChangeItemPrice(int storeId, int itemId, decimal price)
        {
            var answer = new Answer<bool>();
            var a = StoreItems[storeId];
            var storeItem = StoreItems[storeId].FirstOrDefault(x => x.ItemId == itemId);
            if (storeItem == null)
            {
                answer.Error = $"Not fount item: {itemId} in store: {storeId}";
            }
            else
            {
                answer.Data = true;
                storeItem.Price = price;
                StoreItems.SaveChanges();
            }

            return answer;
        }

        public Answer<IDbItem> CreateItem(string itemName)
        {
            var answer = new Answer<IDbItem>();

            var item = Items.FirstOrDefault(x => x.Name == itemName);
            if (item != null)
            {
                answer.Error = $"Item already exists";
            }
            else
            {
                var newItem = new Items()
                {
                    Name = itemName
                };
                Items.Add(newItem);
                Items.SaveChanges();

                answer.Data = new DbItem(newItem.Id, newItem.Name);
            }

            return answer;
        }

        public List<IDbItem> GetItems()
        {
            var answer = new List<IDbItem>();
            foreach (var item in Items)
            {
                answer.Add(new DbItem(item.Id, item.Name));
            }
            return answer;
        }

        public List<LABA_5.Database.Models.StoreItems> GetStoreItems()
        {
            return StoreItems.ToList();
        }

        public void ClearDB()
        {
            LABA_5.FileStore.Models.Items.SaveChanges(new List<Items>());
            LABA_5.FileStore.Models.Stores.SaveChanges(new List<Stores>());
            LABA_5.FileStore.Models.StoreItems.SaveChanges(new List<StoreItems>(), new List<Items>());
        }
    }
}
