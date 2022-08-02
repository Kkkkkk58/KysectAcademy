using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input 1 if you are using DB, anything else if you are using CSV");
            string tmp = Console.ReadLine();
            int Mode = 0;
            if (tmp == "1")
                Mode = 1;

            Dao CNF;
            if (tmp == "1")
                CNF = new DatabaseDAO("database.sqlite3");
            else
                CNF = new CSVDAO("shops.csv", "products.csv");

            Console.WriteLine("Products");
            for (int i = 0; i < CNF.products.Count; i++)
                Console.WriteLine(CNF.products[i].id + " " + CNF.products[i].name);


            Console.WriteLine("Shops");
            foreach (var shop in CNF.shops)
                Console.WriteLine(shop.Key + " " + shop.Value);

            Console.WriteLine("Input a number, according to an operation you would like to perform");
            Console.WriteLine("     [1] Add new shop");
            Console.WriteLine("     [2] Add new product");
            Console.WriteLine("     [3] Ship products to a shop");
            Console.WriteLine("     [4] Find a shop that sells product for the cheapest price");
            Console.WriteLine("     [5] Look what you can buy for a specific price in a shop");
            Console.WriteLine("     [6] Buy products from a shop");
            Console.WriteLine("     [7] Search for a perfect deal");
            Console.WriteLine("     [AnyOther] Terminate");

            bool t = true;
            while (t)
            {
                t = false;

                string c = "";
                c = Console.ReadLine();

                if(c == "1")
                {
                    t = true;
                    Console.WriteLine("Input a name of a new shop");
                    Console.Write(" >");
                    string nm = Console.ReadLine();

                    Console.WriteLine(nm);

                    Console.WriteLine("Input ID of a new shop");
                    Console.Write(" >");
                    int id = Int32.Parse(Console.ReadLine());
                    
                    CNF.AddShop(id, nm);
                    Console.WriteLine("Success");
                }

                if (c == "2")
                {
                    t = true;
                    Console.WriteLine("Input a name of a new product");
                    Console.Write(" >");
                    string nm = Console.ReadLine();


                    Console.WriteLine("Input ID of a new product");
                    Console.Write(" >");
                    int id = Int32.Parse(Console.ReadLine());

                    CNF.AddProduct(id, nm);
                    Console.WriteLine("Success");
                }

                if (c == "3")
                {
                    t = true;
                    Console.WriteLine("Input a shop ID");
                    Console.Write(" >");
                    int ShpID = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Input a product ID");
                    Console.Write(" >");
                    int PrdID = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Input amount of products");
                    Console.Write(" >");
                    int CNT = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Input a price for a product");
                    Console.Write(" >");
                    double PRC = double.Parse(Console.ReadLine());

                    CNF.ShipProductsToShop(PrdID, ShpID, CNT, PRC);
                    Console.WriteLine("Success");
                }

                if (c == "4")
                {
                    t = true;
                    Console.WriteLine("Input a product ID");
                    Console.Write(" >");
                    int PrdID = Int32.Parse(Console.ReadLine());

                    Tuple<int, double> ans = CNF.FindCheapestPrice(PrdID);

                    Console.WriteLine("In a Shop " + ans.Item1 + " it sells cheapest for " + ans.Item2 + "$");
                }

                if (c == "5")
                {
                    t = true;
                    Console.WriteLine("Input a shop ID");
                    Console.Write(" >");
                    int ShpID = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Input an amount of money you could spend");
                    Console.Write(" >");
                    int cash = Int32.Parse(Console.ReadLine());

                    List<Tuple<int, int>> ans = CNF.Availibilities(ShpID, cash);

                    for (int i = 0; i < ans.Count; i++)
                        Console.WriteLine(ans[i].Item2 + " units of a product by ID" + ans[i].Item1); 
                }

                if (c == "6")
                {
                    t = true;
                    Console.WriteLine("Input a shop ID");
                    Console.Write(" >");
                    int ShpID = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Input a product ID");
                    Console.Write(" >");
                    int PrdID = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Input an amount of units of a product you wish to buy");
                    Console.Write(" >");
                    int CNT = Int32.Parse(Console.ReadLine());

                    double ans = CNF.BuyProduct(PrdID, CNT, ShpID);

                    Console.WriteLine("Transaction successfull - " + ans + "$");
                }

                if (c == "7")
                {
                    t = true;
                    Console.WriteLine("How many different products you want?");
                    Console.Write(" >");
                    int N = Int32.Parse(Console.ReadLine());

                    List<Tuple<int, int>> prdcts = new List<Tuple<int, int>>();
                    for(int i = 0; i < N; i++)
                    {
                        Console.WriteLine("Input a product ID");
                        Console.Write(" >");
                        int PrdID = Int32.Parse(Console.ReadLine());


                        Console.WriteLine("How many");
                        Console.Write(" >");
                        int CNT = Int32.Parse(Console.ReadLine());
                        prdcts.Add(new Tuple<int, int>(PrdID, CNT));
                    }


                    Tuple<int, double> ans = CNF.PerfectDeal(prdcts);

                    Console.WriteLine(ans.Item2 + "$ in a shop " + ans.Item1 + " is the best deal you could have");
                }

                
            }

        }
    }
}
