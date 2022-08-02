using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace LABA_5.FileStore.Models
{
    public class StoreItems : IEnumerable<LABA_5.Database.Models.StoreItems>
    {
        private static string path = "items.csv";
        private Items items { get; set; }

        public StoreItems(Items items)
        {
            storeItems = new List<Database.Models.StoreItems>();
            this.items = items;
            if (!File.Exists(path))
                File.WriteAllText(path, "");
            foreach (var line in File.ReadLines(path))
            {
                var itemId = Convert.ToInt32(line.Split(',')[0]);
                var itemName = line.Split(',')[1];

                var data = line.Split(',');
                for (var i = 2; i < data.Length; i += 3)
                {
                    storeItems.Add(new Database.Models.StoreItems()
                    {
                        ItemId = itemId,
                        StoreId = Convert.ToInt32(data[i]),
                        AvailableValue = Convert.ToInt32(data[i+1]),
                        Price = Convert.ToDecimal(data[i+2])
                    });
                }
                
            }
        }

        public List<LABA_5.Database.Models.StoreItems> storeItems { get; set; }

        #region this
        public List<LABA_5.Database.Models.StoreItems> this[int storeId]
        {
            get
            {
                return storeItems.Where(x => x.StoreId == storeId).ToList();
            }
        }
        #endregion

        public void Add(LABA_5.Database.Models.StoreItems item)
        {
            var maxId = (storeItems.Count > 0) ? storeItems.Max(x => x.Id) : 1;
            item.Id = maxId + 1;
            storeItems.Add(item);
        }

        public void SaveChanges()
        {
            List<string> lines = new List<string>();
            foreach (var item in items)
            {
                var line = $"{item.Id},{item.Name}";
                foreach (var storeItem in storeItems.Where(x => x.ItemId == item.Id))
                {
                    line += $",{storeItem.StoreId},{storeItem.AvailableValue},{storeItem.Price.ToString()}";
                }
                lines.Add(line);
            }
            File.WriteAllLines(path, lines);
        }

        public static void SaveChanges(List<LABA_5.Database.Models.StoreItems> storeItems, List<LABA_5.Database.Models.Items> items)
        {
            List<string> lines = new List<string>();
            foreach (var item in items)
            {
                var line = $"{item.Id},{item.Name}";
                foreach (var storeItem in storeItems.Where(x => x.ItemId == item.Id))
                {
                    line += $",{storeItem.StoreId},{storeItem.AvailableValue},{storeItem.Price.ToString()}";
                }
                lines.Add(line);
            }
            File.WriteAllLines(path, lines);
        }

        #region IEnumerator
        public IEnumerator<Database.Models.StoreItems> GetEnumerator()
        {
            return storeItems.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return storeItems.GetEnumerator();
        }
        #endregion
    }
}
