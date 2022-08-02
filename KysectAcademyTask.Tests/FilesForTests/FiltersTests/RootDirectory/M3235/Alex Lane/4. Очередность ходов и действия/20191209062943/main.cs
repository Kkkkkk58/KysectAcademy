using System;

namespace Lab2 {
    internal class Program
    {
        public static void Main(string[] args)
        {
            UnitStack marineStack = new UnitStack(new Marine(), 10);
            UnitStack ghostStack = new UnitStack(new Ghost(), 5);
            Army firstArmy = new Army();
            Army secondArmy = new Army();
      
            firstArmy.AddStack(marineStack);
            firstArmy.AddStack(ghostStack);
            firstArmy.AddStack(new UnitStack(new Marauder(), 7));
            firstArmy.ShowArmy();
      
            secondArmy.AddStack(new UnitStack(new Marine(), 15));
            secondArmy.AddStack(new UnitStack(new Medivac(), 2));
            secondArmy.ShowArmy();
      
            Console.WriteLine();

            Battle btl = new Battle(new BattleArmy(firstArmy), new BattleArmy(secondArmy));
        }
    }
}
