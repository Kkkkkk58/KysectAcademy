using System;
namespace LABA_5.API.Interfaces
{
    public interface IStoreItem : IItem
    {
        decimal Price { get; set; }
        int AvailableValue { get; set; }
        bool Available(int value);
    }
}
