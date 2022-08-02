using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_6__Filter_.API.Interfaces
{
    public interface IProduct
    {
        decimal Price { get; set; }
        int Size { get; set; }
        Color Color { get; set; }
        ProductType Type { get; set; }
    }
}
