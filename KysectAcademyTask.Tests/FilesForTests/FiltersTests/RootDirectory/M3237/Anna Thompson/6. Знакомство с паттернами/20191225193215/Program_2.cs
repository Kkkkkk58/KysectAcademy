using LABA_6__Filter_.API.Interfaces;
using LABA_6__Filter_.API.Models;
using LABA_6__Filter_.Filters;
using System;
using System.Collections.Generic;

namespace LABA_6__Filter_
{
    class Program
    {
        static void Main(string[] args)
        {
            Go();
            Console.ReadLine();
        }

        static void Go()
        {
            List<IProduct> products = new List<IProduct>()
            {
                new Product
                {
                    Color = Color.Red,
                    Type = ProductType.Boots,
                    Price = 5000,
                    Size = 41
                },
                new Product
                {
                    Color = Color.Black,
                    Type = ProductType.Boots,
                    Price = 5000,
                    Size = 41
                },
                new Product
                {
                    Color = Color.Black,
                    Type = ProductType.Jacket,
                    Price = 3000,
                    Size = 52
                },
                new Product
                {
                    Color = Color.Brown,
                    Type = ProductType.Hat,
                    Price = 1000,
                    Size = 15
                },
            };

            IFilter bootsFilter = new TypeFilter(ProductType.Boots);
            IFilter redFilter = new ColorFilter(Color.Red);

            bootsFilter.SetNext(redFilter);

            var filtered = bootsFilter.FilterOut(products);

            foreach(var item in filtered)
            {
                Console.WriteLine($"Price: {item.Price}; Size: {item.Size}; Color: {item.Color.ToString()}; Type: {item.Type.ToString()}");
            }
        }
    }
}
