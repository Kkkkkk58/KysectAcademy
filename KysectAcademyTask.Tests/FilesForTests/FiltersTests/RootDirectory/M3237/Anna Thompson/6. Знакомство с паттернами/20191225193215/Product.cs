using LABA_6__Filter_.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_6__Filter_.API.Models
{
    public class Product : IProduct
    {
        public decimal Price { get; set; }
        public int Size { get; set; }
        public Color Color { get; set; }
        public ProductType Type { get; set; }
    }
}
