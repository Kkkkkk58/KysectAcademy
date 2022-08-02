using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    class ProductsCSVData : IProductsData
    {
        List<Product> loadedproducts;

        public ProductsCSVData(string path)
        {
            loadedproducts = new List<Product>();
            string[] productslines = System.IO.File.ReadAllLines(path); // (@"products.csv")

            for (int i = 0; i < productslines.Length; i++)
            {
                string[] fields = productslines[i].Split(',');
                loadedproducts.Add(new Product(fields));
            }
        }

        List<Product> IProductsData.LoadProducts()
        {
            return loadedproducts;
        }
    }
}
