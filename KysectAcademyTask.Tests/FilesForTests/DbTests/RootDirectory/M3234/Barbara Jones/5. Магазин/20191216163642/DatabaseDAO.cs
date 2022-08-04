using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Lab5
{
    class DatabaseDAO : Dao
    {
        public DatabaseDAO(string path)
        {
            Database DB = new Database(path);
            DB.OpenConnection();
            SQLiteCommand cmd;
            SQLiteDataReader tbl;
            string query;

            shops = new Dictionary<int, string>();
            query = "SELECT * FROM shops";
            cmd = new SQLiteCommand(query, DB.myConnection);
            tbl = cmd.ExecuteReader();

            if (tbl.HasRows)
            {
                while (tbl.Read())
                {
                    int ShopID = Convert.ToInt32(tbl["ID"]);
                    string ShopName = string.Format("{0}", tbl["Name"]);

                    shops.Add(ShopID, ShopName);
                }
            }

            products = new List<Product>();
            query = "SELECT * FROM products";
            cmd = new SQLiteCommand(query, DB.myConnection);
            tbl = cmd.ExecuteReader();

            if (tbl.HasRows)
            {
                while (tbl.Read())
                {
                    int ProductID = Convert.ToInt32(tbl["ID"]);
                    string ProductName = string.Format("{0}", tbl["Name"]);

                    products.Add(new Product(ProductID, ProductName));
                }
            }

            shipproductsfromDB(DB);
            DB.CloseConnection();
        }

        public void shipproductsfromDB(Database DB)
        {
            string query = "SELECT * FROM selling_at";
            SQLiteCommand cmd = new SQLiteCommand(query, DB.myConnection);
            SQLiteDataReader tbl = cmd.ExecuteReader();
            if (tbl.HasRows)
            {
                while (tbl.Read())
                {
                    int ProductID = Convert.ToInt32(tbl["ProductID"]);
                    int ShopID = Convert.ToInt32(tbl["ShopID"]);
                    int Count = Convert.ToInt32(tbl["Quantity"]);
                    double Price = Convert.ToDouble(tbl["Price"]);
                    ShipProductsToShop(ProductID, ShopID, Count, Price);
                }
            }
        }

    }
}
