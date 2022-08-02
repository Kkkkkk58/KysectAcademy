using System;
using LABA_5.API.Interfaces;

namespace LABA_5.API.Models
{
    public class DbItem : IDbItem
    {
        public DbItem(int Id, string Name)
        {
            this.ItemId = Id;
            this.Name = Name;
        }

        public string Name { get; set; }
        public int ItemId { get; set; }
    }
}
