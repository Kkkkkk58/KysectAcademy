using System;
using System.Collections.Generic;
using LABA_5.API;
using LABA_5.Database.API;
using System.Linq;
using LABA_5.API.Interfaces;
using LABA_5.API.Models;
using LABA_5.Database.Models;

namespace LABA_5.Database.Modul.Store
{
    public class DatabaseStore : LABA_5.Database.API.Database, LABA_5.Database.API.Interfaces.IStore
    {
        public DatabaseStore() : base()
        {
        }

        public Answer<bool> AddItems(int storeId, List<ICheckItem> storeItems)
        {
            lock (DbLock)
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    var answer = new Answer<bool>();

                    var store = db.Stores.FirstOrDefault(x => x.Id == storeId);
                    if (store == null)
                    {
                        answer.Error = $"Not found store: {storeId}";
                        return answer;
                    }

                    foreach(var newItem in storeItems)
                    {
                        var item = db.StoreItems
                            .Where(x => x.StoreId == storeId)
                            .FirstOrDefault(x => x.ItemId == newItem.ItemId);
                        if (item == null)
                        {
                            db.StoreItems.Add(new Models.StoreItems()
                            {
                                StoreId = storeId,
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
                    db.SaveChanges();
                    answer.Data = true;
                    return answer;
                }
            }
        }

        public Answer<bool> RemoveItems(int storeId, List<ICheckItem> storeItems)
        {
            lock (DbLock)
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    var answer = new Answer<bool>();

                    var store = db.Stores.FirstOrDefault(x => x.Id == storeId);
                    if (store == null)
                    {
                        answer.Error = $"Not found store: {storeId}";
                        return answer;
                    }

                    foreach (var item in storeItems)
                    {
                        var storeItem = db.StoreItems
                            .FirstOrDefault(x => x.StoreId == storeId &&
                            x.ItemId == item.ItemId);
                        if (storeItem == null || storeItem.AvailableValue < item.Value)
                        {
                            answer.Error = $"Not enough goods in stock: {item.ItemId} in store {storeId}";
                            return answer;
                        }
                        storeItem.AvailableValue -= item.Value;
                    }
                    db.SaveChanges();
                    answer.Data = true;
                    return answer;
                }
            }
        }

        public Answer<IStore> CreateStore(string name)
        {
            lock (DbLock)
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    var answer = new Answer<IStore>();

                    var store = db.Stores.FirstOrDefault(x => x.Name == name);
                    if (store != null)
                    {
                        answer.Error = "Store with the same name already exists";
                    }
                    else
                    {
                        db.Stores.Add(new Models.Stores()
                        {
                            Name = name
                        });
                        db.SaveChanges();

                        answer.Data = GetStore(name);
                    }
                    
                    return answer;
                }
            }
        }

        public IStore GetStore(string name)
        {
            lock (DbLock)
            {
                using(DatabaseContext db = new DatabaseContext())
                {
                    var store = db.Stores.FirstOrDefault(x => x.Name == name);
                    if (store == null) return null;
                    var items = db.StoreItems
                        .Where(x => x.StoreId == store.Id)
                        .ToList();
                    var storeItems = new List<LABA_5.API.Interfaces.IStoreItem>();
                    items.ForEach(x =>
                    {                  
                        storeItems.Add(new StoreItem(x.ItemId, x.Price, x.AvailableValue));
                    });
                    return new LABA_5.API.Store(name, store.Id, storeItems, this);
                }
            }
        }

        public List<IStore> GetStores()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var answer = new List<IStore>();
                foreach (var name in db.Stores.Select(x => x.Name))
                {
                    answer.Add(GetStore(name));
                }
                return answer;
            }
        }

        public Answer<bool> ChangeItemPrice(int storeId, int itemId, decimal price)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var answer = new Answer<bool>();

                var storeItem = db.StoreItems.FirstOrDefault(x => x.StoreId == storeId && x.ItemId == itemId);
                if (storeItem == null)
                {
                    answer.Error = $"Not fount item: {itemId} in store: {storeId}";
                }
                else
                {
                    answer.Data = true;
                    storeItem.Price = price;
                    db.SaveChanges();
                }

                return answer;
            }
        }

        public Answer<IDbItem> CreateItem(string itemName)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var answer = new Answer<IDbItem>();

                var item = db.Items.FirstOrDefault(x => x.Name == itemName);
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
                    db.Items.Add(newItem);
                    db.SaveChanges();

                    answer.Data = new DbItem(newItem.Id, newItem.Name);
                }

                return answer;
            }
        }

        public List<IDbItem> GetItems()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var answer = new List<IDbItem>();
                foreach(var item in db.Items)
                {
                    answer.Add(new DbItem(item.Id, item.Name));
                }
                return answer;
            }
        }

        public List<LABA_5.Database.Models.StoreItems> GetStoreItems()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return db.StoreItems.ToList();
            }
        }

        /*public List<LABA_5.Database.Models.Stores> GetBaseStores()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return db.Stores.ToList();
            }
        }*/

        public void ClearDB()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                foreach(var store in db.Stores)
                {
                    db.Stores.Remove(store);
                }

                foreach(var item in db.Items)
                {
                    db.Items.Remove(item);
                }

                foreach(var storeItem in db.StoreItems)
                {
                    db.StoreItems.Remove(storeItem);
                }

                db.SaveChanges();
            }
        }
    }
}
