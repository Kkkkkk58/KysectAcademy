using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class Product
    {
        public int id;
        public string name;
        public List<Tuple<int, int, double>> being_sold_at;

        public Product(string[] fields)
        {
            id = Int32.Parse(fields[0]);
            name = fields[1];
            being_sold_at = new List<Tuple<int, int, double>>();

            int i = 2;
            while (i < fields.Length)
            {
                int shopid = Int32.Parse(fields[i]);
                int count = Int32.Parse(fields[i + 1]);
                //Console.WriteLine(fields[i + 2]);
                //Console.ReadKey();
                double price = double.Parse(fields[i + 2].Replace('.', ','));
                being_sold_at.Add(new Tuple<int, int, double>(shopid, count, price));
                i += 3;
            }
        }

        public Product(int ID, string NM)
        {
            id = ID;
            name = NM;
            being_sold_at = new List<Tuple<int, int, double>>();
        }
    }
}
