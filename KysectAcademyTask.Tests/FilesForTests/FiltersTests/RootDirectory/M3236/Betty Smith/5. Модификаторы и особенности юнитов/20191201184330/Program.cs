using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Heroes3;

        static class Program
        {
    /// <summary>
    /// Главная точка входа для приложения.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Writer writer = new Writer();
        /*Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Form1());*/
        List<UnitStack> stack = new List<UnitStack> {
         new UnitStack("ARBALET", 999), new UnitStack("SKELETON", 965)};
        List<UnitStack> NEWstack = new List<UnitStack> { new UnitStack("HYDRA", 999), new UnitStack("ANGEL", 965), new UnitStack("DEVIL", 98)};

        ArmyClass ss = new ArmyClass(stack);
        ArmyClass NEWss = new ArmyClass(NEWstack);

        Battle GAME = new Battle(NEWss, ss);
        int b = 7;
        //GAME.ShowInfo();
        /*    foreach (BattleUnitStack u in GAME.ArmyQueue)
        {
            Console.WriteLine(u.ShowBattleStats());
        }*/
        writer.Show(NEWss);
        writer.Show(ss);
    }
}
