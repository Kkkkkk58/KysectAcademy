using LABA_5.API.Interfaces;

namespace LABA_5.API
{
    public class StoreItem : IStoreItem
    {
        public decimal Price { get; set; }
        public int AvailableValue { get; set; }
        public int ItemId { get; set; }

        public StoreItem(int ItemId, decimal Price, int AvailableValue)
        {
            this.ItemId = ItemId;
            this.Price = Price;
            this.AvailableValue = AvailableValue;
        }

        public static bool operator !=(StoreItem item, int id)
        {
            return item.ItemId != id;
        }
        
        public static bool operator ==(StoreItem item, int id)
        {
            return item != id;
        }

        public bool Available(int value)
        {
            return Price > 0 && AvailableValue >= value;
        }
    }
}