using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Lab5
{
    class ProductsData : IProductsData
    {
        List<Product> loadedproducts;

        public ProductsData(string path)
        {
            loadedproducts = new List<Product>();
            string[] productslines = System.IO.File.ReadAllLines(path); // (@"products.csv")

            for (int i = 0; i < productslines.Length; i++)
            {
                string[] fields = productslines[i].Split(',');
                loadedproducts.Add(new Product(fields));
            }
        }

        public ProductsData(Database DB)
        {
            loadedproducts = new List<Product>();
            string query = "SELECT * FROM products";
            SQLiteCommand cmd = new SQLiteCommand(query, DB.myConnection);
            SQLiteDataReader tbl = cmd.ExecuteReader();
            if (tbl.HasRows)
            {
                while (tbl.Read())
                {
                    int ProductID = (int)tbl["ID"];
                    string ProductName = string.Format("{0}", tbl["Name"]);

                    loadedproducts.Add(new Product(ProductID, ProductName));
                }
            }

        }

        List<Product> IProductsData.LoadProducts()
        {
            return loadedproducts;
        }
    }
}
