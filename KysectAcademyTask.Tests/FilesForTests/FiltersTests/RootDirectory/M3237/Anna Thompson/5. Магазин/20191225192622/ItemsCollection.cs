using System.Collections.Generic;

namespace LABA_5.API
{
    public class ItemsCollection : IItemsCollection
    {
        private Dictionary<string, IItem> _items { get; set; }
        private  Dictionary<string, decimal> _prices { get; set; }

        public ItemsCollection()
        {
            _items = new Dictionary<string, IItem>();
            _prices = new Dictionary<string, decimal>();
        }

        public bool ContainsItem(string ItemName)
        {
            return _items.ContainsKey(ItemName);
        }
        
        public decimal GetItemPrice(string ItemName)
        {
            if (!ContainsItem(ItemName)) return 0;
            if (_prices.ContainsKey(ItemName))
            {
                return _prices[ItemName];
            }
            else
            {
                return 0;
            }
        }
    }
}