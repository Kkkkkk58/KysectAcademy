using System;

namespace LABA_2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var location_data = "../../../data.json";
                SetFractions sf = new SetFractions();
                sf.LoadFromFile(location_data);
                Console.WriteLine(sf.ToString());
                Console.WriteLine($"Max: {sf.Max()}");
                Console.WriteLine($"Min: {sf.Min()}");
                Console.WriteLine($"More: {sf.MoreCount(new RationalFraction(1, 5))}");
                Console.WriteLine($"Smaller: {sf.SmallerCount(new RationalFraction(1, 2))}");
                Console.WriteLine();

                Polynomial pl = new Polynomial(sf);
                Console.WriteLine($"Polynomial (10):\n{pl.Result(10)}");
                var a = pl + pl;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
