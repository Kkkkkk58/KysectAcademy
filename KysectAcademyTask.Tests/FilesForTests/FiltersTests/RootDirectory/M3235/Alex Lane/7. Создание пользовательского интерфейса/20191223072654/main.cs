using System;

namespace Lab2 {
    internal class Program
    {
        public static void Main(string[] args) {
            ArmyPicking picker = new ArmyPicking();
            Loader loader = new Loader();
            
            picker.units = loader.ToLoad();

            picker.Pick(0);
            picker.Pick(1);
            
            Console.WriteLine();

            Battle btl = new Battle(new BattleArmy(picker.army[0]), new BattleArmy(picker.army[1]));
        }
    }
}
