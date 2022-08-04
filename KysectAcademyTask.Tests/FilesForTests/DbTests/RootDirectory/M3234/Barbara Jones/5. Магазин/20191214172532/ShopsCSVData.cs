using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    class ShopsCSVData : IShopsData
    {
        Dictionary<int, string> loadedshops;

        public ShopsCSVData(string path)
        {
            loadedshops = new Dictionary<int, string>();
            string[] shoplines = System.IO.File.ReadAllLines(path); ; // (@"shops.csv")

            for (int i = 0; i < shoplines.Length; i++)
            {
                string[] fields = shoplines[i].Split(',');
                loadedshops.Add(Int32.Parse(fields[0]), fields[1]);
            }
        }

        Dictionary<int, string> IShopsData.LoadShops()
        {
            return loadedshops;
        }
    }
}
