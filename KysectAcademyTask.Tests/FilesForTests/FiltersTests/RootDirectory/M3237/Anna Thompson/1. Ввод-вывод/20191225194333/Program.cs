using System;
using LABA_1;

namespace LABA_1
{
    class Program
    {
        static void Main(string[] args)
        {
            rome();
            // 1
            Adder.Go();

            // 2
            Console.WriteLine($"Fibonacci (5):");
            Fibonacci.show(5);
            Console.WriteLine("\n");

            // 3
            Console.WriteLine("ROME:");
            Console.WriteLine("MIV = " + Rome.ConvertToInt32("MIV"));
            Console.WriteLine("III = " + Rome.ConvertToInt32("III"));
            Console.WriteLine("IV = " + Rome.ConvertToInt32("IV"));
            Console.WriteLine("MMXIX = " + Rome.ConvertToInt32("MMXIX"));
        }

        static void rome()
        {
            while (true)
            {
                var str = Console.ReadLine();                
                Console.WriteLine(Rome.ConvertToInt32(str));
            }
        }
    }
}
