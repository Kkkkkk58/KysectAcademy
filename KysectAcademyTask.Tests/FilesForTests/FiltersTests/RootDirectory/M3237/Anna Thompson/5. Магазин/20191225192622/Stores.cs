using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using LABA_5.Database.Models;
using System.Linq;
using System.IO;

namespace LABA_5.FileStore.Models
{
    public class Stores : IEnumerable<LABA_5.Database.Models.Stores>
    {
        private static string path = "stores.csv";

        public Stores()
        {
            stores = new List<Database.Models.Stores>();
            if (!File.Exists(path))
                File.WriteAllText(path, "");
            foreach(var line in File.ReadLines(path))
            {
                stores.Add(new Database.Models.Stores()
                {
                    Id = Convert.ToInt32(line.Split(',')[0]),
                    Name = line.Split(',')[1]
                });
            }
        }

        public List<LABA_5.Database.Models.Stores> stores { get; set; }

        #region this
        public LABA_5.Database.Models.Stores this[int storeId]
        {
            get
            {
                return stores.FirstOrDefault(x => x.Id == storeId);
            }
        }

        public LABA_5.Database.Models.Stores this[string storeName]
        {
            get
            {
                return stores.FirstOrDefault(x => x.Name == storeName);
            }
        }
        #endregion

        public void Add(LABA_5.Database.Models.Stores store)
        {
            var maxId = (stores.Count > 0) ? stores.Max(x => x.Id) : 1;
            store.Id = maxId + 1;
            stores.Add(store);
        }

        public void SaveChanges()
        {
            List<string> lines = new List<string>();
            foreach(var store in stores)
            {
                lines.Add($"{store.Id},{store.Name}");
            }
            File.WriteAllLines(path, lines);
        }

        public static void SaveChanges(List<LABA_5.Database.Models.Stores> stores)
        {
            List<string> lines = new List<string>();
            foreach (var store in stores)
            {
                lines.Add($"{store.Id},{store.Name}");
            }
            File.WriteAllLines(path, lines);
        }

        #region IEnumerator
        public IEnumerator<Database.Models.Stores> GetEnumerator()
        {
            return stores.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return stores.GetEnumerator();
        }
        #endregion
    }
}
