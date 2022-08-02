using System;
namespace LABA_5.API.Models
{
    public class CheckItem : ICheckItem
    {
        public int Value { get; set; }
        public int ItemId { get; set; }

        public CheckItem(int ItemId, int Value)
        {
            this.ItemId = ItemId;
            this.Value = Value;
        }
    }
}
