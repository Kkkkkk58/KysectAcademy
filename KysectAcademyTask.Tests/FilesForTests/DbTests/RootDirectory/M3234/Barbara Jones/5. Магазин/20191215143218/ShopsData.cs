using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Lab5
{
    class ShopsData : IShopsData
    {
        Dictionary<int, string> loadedshops;

        public ShopsData(string path)
        {
            loadedshops = new Dictionary<int, string>();
            string[] shoplines = System.IO.File.ReadAllLines(path); ; // (@"shops.csv")

            for (int i = 0; i < shoplines.Length; i++)
            {
                string[] fields = shoplines[i].Split(',');
                loadedshops.Add(Int32.Parse(fields[0]), fields[1]);
            }
        }

        public ShopsData(Database DB)
        {
            loadedshops = new Dictionary<int, string>();
            string query = "SELECT * FROM shops";
            SQLiteCommand cmd = new SQLiteCommand(query, DB.myConnection);
            SQLiteDataReader tbl = cmd.ExecuteReader();
            if(tbl.HasRows)
            {
                while (tbl.Read())
                {
                    int ShopID = (int)tbl["ID"];
                    string ShopName = string.Format("{0}", tbl["Name"]);

                    loadedshops.Add(ShopID, ShopName);
                }
            }
        }

        Dictionary<int, string> IShopsData.LoadShops()
        {
            return loadedshops;
        }
    }
}
