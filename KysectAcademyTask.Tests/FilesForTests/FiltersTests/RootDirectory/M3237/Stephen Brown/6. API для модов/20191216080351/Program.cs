using System;

namespace Laba_v2
{
    class Program
    {
        static void Main(string[] args)
        {
            int w, r;

            Army x1 = new Army();

            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("Выберита юнита для первой армии:");

                Global_Unit.Show();

                Console.WriteLine("Если закончили, введите -1");

                r = Convert.ToInt32(Console.ReadLine());

                if (r == -1)
                    break;

                if (r >= Global_Unit.Lists.Count)
                {
                    Console.Clear();
                    Console.WriteLine("Число вне диапазона");
                    Console.ReadKey();
                    Console.Clear();
                    i--;
                    continue;
                }

                Console.Clear();

                Console.WriteLine("Кол-во юнитов:");

                w = Convert.ToInt32(Console.ReadLine());

                x1.Add(new UnitsStack(new Unit(Global_Unit.Lists[r]), w));

                Console.Clear();
            }

            Console.Clear();

            BattleArmy r1 = new BattleArmy(x1);

            Army x2 = new Army();

            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("Выберита юнита для второй армии:");

                Global_Unit.Show();

                Console.WriteLine("Если закончили, введите -1");

                r = Convert.ToInt32(Console.ReadLine());

                if (r == -1)
                    break;

                if (r >= Global_Unit.Lists.Count)
                {
                    Console.Clear();
                    Console.WriteLine("Число вне диапазона");
                    Console.ReadKey();
                    Console.Clear();
                    i--;
                    continue;
                }

                Console.Clear();

                Console.WriteLine("Кол-во юнитов:");

                w = Convert.ToInt32(Console.ReadLine());

                x2.Add(new UnitsStack(new Unit(Global_Unit.Lists[r]), w));

                Console.Clear();
            }
            Console.Clear();

            BattleArmy r2 = new BattleArmy(x2);

            Battle x = new Battle(r1, r2);

            x.Start();
        }
    }
}
