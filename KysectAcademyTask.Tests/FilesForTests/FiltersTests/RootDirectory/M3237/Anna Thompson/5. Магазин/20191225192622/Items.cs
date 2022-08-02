using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;
using System.IO;

namespace LABA_5.FileStore.Models
{
    public class Items : IEnumerable<LABA_5.Database.Models.Items>
    {
        private static string path = "items.csv";

        public Items()
        {
            items = new List<Database.Models.Items>();
            newItems = new List<Database.Models.Items>();
            if (!File.Exists(path))
                File.WriteAllText(path, "");
            foreach (var line in File.ReadLines(path))
            {
                var itemId = Convert.ToInt32(line.Split(',')[0]);
                var itemName = line.Split(',')[1];

                items.Add(new Database.Models.Items()
                {
                    Id = itemId,
                    Name = itemName
                });
            }
        }

        public List<LABA_5.Database.Models.Items> items { get; set; }
        public List<LABA_5.Database.Models.Items> newItems { get; set; }

        #region this
        public LABA_5.Database.Models.Items this[string itemName]
        {
            get
            {
                return items.FirstOrDefault(x => x.Name == itemName);
            }
        }

        public LABA_5.Database.Models.Items this[int itemId]
        {
            get
            {
                return items.FirstOrDefault(x => x.Id == itemId);
            }
        }
        #endregion

        public void Add(LABA_5.Database.Models.Items item)
        {
            var maxId = (items.Count > 0) ? items.Max(x => x.Id) : 1;
            item.Id = maxId + 1;
            items.Add(item);
            newItems.Add(item);
        }

        public void SaveChanges()
        {
            List<string> lines = new List<string>();
            foreach (var item in newItems)
            {
                lines.Add($"{item.Id},{item.Name}");
            }
            File.AppendAllLines(path, lines);
            newItems.Clear();
        }

        public static void SaveChanges(List<LABA_5.Database.Models.Items> items)
        {
            File.WriteAllText(path, "");
            List<string> lines = new List<string>();
            foreach (var item in items)
            {
                lines.Add($"{item.Id},{item.Name}");
            }
            File.AppendAllLines(path, lines);
        }

        #region IEnumerator
        public IEnumerator<Database.Models.Items> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
        #endregion
    }
}
