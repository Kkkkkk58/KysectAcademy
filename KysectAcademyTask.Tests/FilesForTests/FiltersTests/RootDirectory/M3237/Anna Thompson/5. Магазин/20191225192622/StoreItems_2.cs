using System;
namespace LABA_5.Database.Models
{
    public class StoreItems
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int StoreId { get; set; }
        public decimal Price { get; set; }
        public int AvailableValue { get; set; }
    }
}
