using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    class CSVDAO : Dao
    {
        public CSVDAO(string shoppath, string productpath)
        {
            shops = new Dictionary<int, string>();
            string[] shoplines = System.IO.File.ReadAllLines(shoppath); //@"shops.csv")

            for (int i = 0; i < shoplines.Length; i++)
            {
                string[] fields = shoplines[i].Split(',');
                shops.Add(Int32.Parse(fields[0]), fields[1]);
            }

            products = new List<Product>();
            string[] productslines = System.IO.File.ReadAllLines(productpath); // (@"products.csv")

            for (int i = 0; i < productslines.Length; i++)
            {
                string[] fields = productslines[i].Split(',');
                products.Add(new Product(fields));
            }
        }

    }
}
