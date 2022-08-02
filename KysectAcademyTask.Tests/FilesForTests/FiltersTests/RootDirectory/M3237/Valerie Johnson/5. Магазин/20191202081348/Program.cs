using System;
using Lab5.DataModel;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;


namespace Lab5
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
            : base("name=ApplicationContext")
        {

        }
        public DbSet<Market> Markets { get; set; }
        public DbSet<Lot> Lots { get; set; }
    }
    public class Market
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Lot
    {
        public int Id { get; set; }
        public int MarketId { get; set; }
        public string ProductName { get; set; }
        public int Number { get; set; }
        public double Price { get; set; }
    }

    public static class Redactor
    {
        public static void CreateMarket(string Name)
        {
            using ApplicationContext db = new ApplicationContext();
            var marketexist = db.Markets.Where(m => m.Name == Name);
            if (marketexist.Count() == 0)
            {
                Market market = new Market
                {
                    Name = Name
                };
                db.Markets.Add(market);
                db.SaveChanges();
            }
        }
        public static void Supply(int MarketId, string ProductName, int Number, double Price)
        {
            using ApplicationContext db = new ApplicationContext();
            if (MarketId <= db.Markets.Count())
            {
                var lotexist = db.Lots.Where(l => l.MarketId == MarketId && l.ProductName == ProductName);
                if (lotexist.Count() == 0)
                {
                    Lot lot = new Lot
                    {
                        MarketId = MarketId,
                        ProductName = ProductName,
                        Number = Number,
                        Price = Price
                    };
                    db.Lots.Add(lot);
                    db.SaveChanges();
                }
            }
        }
    }
    public static class Requester
    {
        public static Market GetMarketWithMinPrice(string ProductName)
        {
            using ApplicationContext db = new ApplicationContext();
            double min = db.Lots.Where(l => l.ProductName == ProductName).Min(l => l.Price);
            var lots = db.Lots.Where(l => l.Price == min && l.ProductName == ProductName);
            foreach (var lot in lots)
            {
                var markets = db.Markets.Where(m => m.Id == lot.MarketId);
                foreach (var market in markets)
                {
                    return market;
                }
            }
            return null;
        }
        public static void AvailableProducts(int MarketId, double sum)
        {
            using ApplicationContext db = new ApplicationContext();
            var lots = db.Lots.Where(l => l.MarketId == MarketId);
            foreach (var lot in lots)
            {
                var market = db.Markets.First(m => m.Id == lot.MarketId);
                string name = market.Name;
                double n = Math.Floor(sum / lot.Price);
                if (n <= lot.Number)
                {
                    Console.WriteLine("В магазине \"{2}\" вы можете купить {0} шт. продукта {1}", n, lot.ProductName, name);
                }
                else
                {
                    Console.WriteLine("В магазине \"{2}\" вы можете купить {0} шт. продукта {1}", lot.Number, lot.ProductName, name);
                }
            }
        }
        public static void BuyProduct(string ProductName, int Number, int MarketId)
        {
            using ApplicationContext db = new ApplicationContext();
            var lots = db.Lots.Where(p => p.ProductName == ProductName && p.MarketId == MarketId);
            if (lots.Count() == 0)
            {
                Console.WriteLine("Покупка невозможна");
            }
            else
            {
                foreach (var lot in lots)
                {
                    if (lot.Number < Number)
                    {
                        Console.WriteLine("Покупка невозможна");
                    }
                    else
                    {
                        double sum = lot.Price * Number;
                        Console.WriteLine("Сумма покупки составила {0}", sum);
                    }
                }
            }
        }
        public static Market BuySeveralProducts(List<ProductNumber> ProductNumbers)
        {
            using ApplicationContext db = new ApplicationContext();
            List<double> sums = new List<double>();
            double sum = 0;
            for (int i = 1; i <= db.Markets.Count(); i++)
            {
                var lots = db.Lots.Where(l => l.MarketId == i);
                foreach (var ProductNumber in ProductNumbers)
                {
                    var products = lots.Where(l => l.ProductName == ProductNumber.ProductName && l.Number >= ProductNumber.Number);
                    if (products.Count() == 0)
                    {
                        sum = 0;
                        break;
                    }
                    else
                    {
                        foreach (var product in products)
                        {
                            sum += ProductNumber.Number * product.Price;
                        }
                    }
                }
                if (sum != 0)
                {
                    sums.Add(sum);
                    sum = 0;
                }
            }
            int id = sums.IndexOf(sums.Min()) + 1;
            var market = db.Markets.First(m => m.Id == id);
            return market;
        }
    }
    class Program
    {
        static void Main()
        {
            ProductNumber p1 = new ProductNumber("Хлеб", 2);
            ProductNumber p2 = new ProductNumber("Молоко", 1);
            List<ProductNumber> psss = new List<ProductNumber>();
            psss.Add(p1);
            psss.Add(p2);
            Console.WriteLine(Requester.BuySeveralProducts(psss).Name);
            Console.ReadKey();
        }
    }
}
