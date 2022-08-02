using System.Collections.Generic;
using LABA_5.API.Interfaces;
using System.Linq;

namespace LABA_5.API
{
    public class Store : IStore
    {
        public string Name { get; set; }
        public int Id { get; set; }
        private List<IStoreItem> _items { get; set; }
        private LABA_5.Database.API.Interfaces.IStore dbStore { get; set; }

        public Store(string Name, int Id, List<IStoreItem> Items,
            LABA_5.Database.API.Interfaces.IStore DbStore)
        {
            this.Name = Name;
            this.Id = Id;
            _items = Items;
            dbStore = DbStore;
        } 

        private void ChangeItems(List<ICheckItem> storeItems, ChangeType type)
        {
            foreach(var item in storeItems)
            {
                var i = _items.FirstOrDefault(x => x.ItemId == item.ItemId);
                switch (type)
                {
                    case ChangeType.Add:
                        if (i == null)
                            _items.Add(new StoreItem(item.ItemId, 0, item.Value));
                        else
                            i.AvailableValue += item.Value;
                        break;
                    case ChangeType.Remove:
                        i.AvailableValue -= item.Value;
                        break;
                }
            }
        }

        public void AddItems(List<ICheckItem> storeItems)
        {
            var response = dbStore.AddItems(Id, storeItems);
            if (response.Success)
            {
                ChangeItems(storeItems, ChangeType.Add);
            }
            else
            {
                Logger.Error(response.Error);
            }
        }

        public Answer<bool> Buy(List<ICheckItem> storeItems)
        {
            var response = dbStore.RemoveItems(Id, storeItems);

            if (response.Success)
            {
                ChangeItems(storeItems, ChangeType.Remove);
            }

            return response;
        }

        public List<ICheckItem> GetAvailablePurchase(decimal sum)
        {
            throw new System.NotImplementedException();
        }

        public Answer<decimal> GetItemPrice(int itemId)
        {
            var answer = new Answer<decimal>();

            var storeItem = _items.FirstOrDefault(x => x.ItemId == itemId);

            if (storeItem == null || !storeItem.Available(1))
            {
                answer.Error = $"Not fount available item: {itemId}";
            }
            else
            {
                answer.Data = storeItem.Price;
            }

            return answer;
        }

        public Answer<decimal> GetSum(List<ICheckItem> items)
        {
            var answer = new Answer<decimal>();

            foreach (var item in items)
            {
                var storeItem = _items.FirstOrDefault(x => x.ItemId == item.ItemId);

                if (storeItem == null || !storeItem.Available(item.Value))
                {
                    answer.Error = $"Not fount available item: {item.ItemId}";
                    break;
                }
                else
                {
                    answer.Data += storeItem.Price * item.Value;
                }

            }

            return answer;
        }

        public Answer<bool> ChangeItemPrice(int itemId, decimal price)
        {
            var response = dbStore.ChangeItemPrice(Id, itemId, price);
            if (response.Success)
                _items.FirstOrDefault(x => x.ItemId == itemId).Price = price;
            return response;
        }
    }
}