using System;
namespace LABA_5.API.Interfaces
{
    public interface IDbItem : IItem
    {
        string Name { get; set; }
    }
}
