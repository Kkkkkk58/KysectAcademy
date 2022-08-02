using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Lab5
{
    public class Configuration
    {
        IShopsData shopsdata;
        IProductsData productsdata;
        Dictionary<int, string> shops;
        List<Product> products;

        public Configuration(int Mode)
        {
            if (Mode == 2)
            {
                shopsdata = new ShopsData("shops.csv");
                productsdata = new ProductsData("products.csv");

                shops = shopsdata.LoadShops();
                products = productsdata.LoadProducts();
            }
            else
            {
                Database DB = new Database();
                DB.OpenConnection();

                shopsdata = new ShopsData(DB);
                productsdata = new ProductsData(DB);

                shops = shopsdata.LoadShops();
                products = productsdata.LoadProducts();

                shipproductsfromDB(DB);
                DB.CloseConnection();
            }

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
                    int ProductID = (int)tbl["ProductID"];
                    int ShopID = (int)tbl["ShopID"];
                    int Count = (int)tbl["Quantity"];
                    double Price = (double)tbl["Price"];
                    ShipProductsToShop(ProductID, ShopID, Count, Price);
                }
            }
        }

        public void AddShop(int ID, string NM)
        {
            shops.Add(ID, NM);
        }

        public void AddProduct(int ID, string NM)
        {
            products.Add(new Product(ID, NM));
        }

        public int FindProduct(int ProductID)
        {
            int i = 0;
            while (i < products.Count && products[i].id != ProductID)
                i++;

            if (i == products.Count)
                throw new Exception("No product with such ID was found");

            return i;
        }

        public void ShipProductsToShop(int ProductID, int ShopID, int CNT, double PRC)
        {
            int i = FindProduct(ProductID);
            int j = 0;
            while (j < products[i].being_sold_at.Count && products[i].being_sold_at[j].Item1 != ShopID)
                j++;

            if (j == products[i].being_sold_at.Count)
            {
                products[i].being_sold_at.Add(new Tuple<int, int, double>(ShopID, CNT, PRC));
            }
            else
            {
                int OldCNT = products[i].being_sold_at[j].Item2;
                products[i].being_sold_at[j] = new Tuple<int, int, double>(ShopID, OldCNT + CNT, PRC);
            }
        }

        public Tuple<int, double> FindCheapestPrice(int ProductID)
        {
            int i = FindProduct(ProductID);

            if (products[i].being_sold_at.Count == 0)
                throw new Exception("This product is not selling at any availible shops currently");

            int minid = products[i].being_sold_at[0].Item1;
            double minprc = products[i].being_sold_at[0].Item3;

            for (int j = 0; j < products[i].being_sold_at.Count; j++)
                if (products[i].being_sold_at[j].Item3 < minprc)
                {
                    minid = products[i].being_sold_at[j].Item1;
                    minprc = products[i].being_sold_at[j].Item3;
                }

            return new Tuple<int, double>(minid, minprc);
        }

        public List<Tuple<int, int>> Availibilities(int ShopID, double cash)
        {
            List<Tuple<int, int>> avlble = new List<Tuple<int, int>>();
            for (int i = 0; i < products.Count; i++)
                for (int j = 0; j < products[i].being_sold_at.Count; j++)
                    if (products[i].being_sold_at[j].Item1 == ShopID)
                    {
                        Tuple<int, int, double> tmp = products[i].being_sold_at[j];
                        double xx = cash / tmp.Item3;

                        if (tmp.Item2 != 0 && tmp.Item3 < cash)
                            avlble.Add(new Tuple<int, int>(products[i].id, Math.Min(tmp.Item2, (int)Math.Floor(cash / tmp.Item3))));
                    }
            return avlble;
        }

        public double BuyProduct(int ProductID, int Count, int ShopID)
        {
            int i = FindProduct(ProductID);
            int j = 0;
            while (j < products[i].being_sold_at.Count && products[i].being_sold_at[j].Item1 != ShopID)
                j++;

            if (j == products[i].being_sold_at.Count)
                throw new Exception("This product is not selling at this shop");

            if (Count > products[i].being_sold_at[j].Item2)
                throw new Exception("Shop has less specified products than requested");

            int OldCNT = products[i].being_sold_at[j].Item2;
            double PRC = products[i].being_sold_at[j].Item3;

            products[i].being_sold_at[j] = new Tuple<int, int, double>(ShopID, OldCNT - Count, PRC);
            return PRC * Count;
        }

        public Tuple<int, double> PerfectDeal(List<Tuple<int, int>> prdcts_ids)
        {
            int PerfectShopID = -1;
            double PerfectPrice = 2000000000;
            List<Tuple<Product, int>> prdcts = new List<Tuple<Product, int>>();

            for (int i = 0; i < prdcts_ids.Count; i++)
            {
                int k = FindProduct(prdcts_ids[i].Item1);
                prdcts.Add(new Tuple<Product, int>(products[k], prdcts_ids[i].Item2));
            }


            foreach (var shop in shops)
            {
                int ShopID = shop.Key;
                double CurrentPRC = 0;
                bool T = false;

                //Console.WriteLine("checking shop " + ShopID);

                for (int i = 0; i < prdcts.Count; i++)
                {
                    //Console.WriteLine("seeking for a product " + prdcts[i].Item1 + " in this shop");
                    bool t = true;
                    for (int j = 0; j < prdcts[i].Item1.being_sold_at.Count; j++)
                    {
                        Tuple<int, int, double> newshop = prdcts[i].Item1.being_sold_at[j];
                        //Console.WriteLine(newshop.Item1 + " == " + ShopID + " ; " + newshop.Item2 + " <= " + prdcts[i].Item2);
                        if (newshop.Item1 == ShopID && newshop.Item2 >= prdcts[i].Item2)
                        {
                            t = false;
                            CurrentPRC += newshop.Item3 * prdcts[i].Item2;
                        }
                    }

                    if (t)
                    {
                        T = true;
                        break;
                    }
                }

                if (T) continue;
                if (CurrentPRC < PerfectPrice)
                {
                    PerfectPrice = CurrentPRC;
                    PerfectShopID = ShopID;
                }
            }

            if (PerfectShopID == -1)
                throw new Exception("Such deal is not selling anywhere");

            return new Tuple<int, double>(PerfectShopID, PerfectPrice);
        }

    }
}
