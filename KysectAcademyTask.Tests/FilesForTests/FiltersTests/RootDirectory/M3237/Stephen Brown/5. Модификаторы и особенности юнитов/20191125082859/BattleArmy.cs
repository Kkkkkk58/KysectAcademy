using System;
using System.Collections.Generic;
using System.Text;

namespace Laba
{
    public class BattleArmy
    {
        private List<(BattleUnitsStack BattleUS, int GlobInd)> Arm = new List<(BattleUnitsStack, int)>();

        public IList<(BattleUnitsStack BattleUS, int GlobInd)> arm { get { return Arm.ToArray(); } }

        public BattleArmy()
        { }

        public BattleArmy(Army x)
        {
            for (int i = 0; i < x.arm.Count; i++)
            {
                Arm.Add((new BattleUnitsStack(x.arm[i]), GInd.ind));
                GInd.ind++;
            }
    
        }

        public BattleArmy(BattleArmy x)
        {
            for (int i = 0; i < x.arm.Count; i++)
            {
                Arm.Add(x.arm[i]);
            }
        }

        public void Add(BattleUnitsStack x, int ind)
        {
            if (Arm.Count < 9)
            {
                Arm.Add((x, GInd.ind));
                GInd.ind++;
            }
            else
            {
                Console.WriteLine("Army is full");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public void Show()
        {
            for (int i = 0; i < Arm.Count; i++)
            {
                Console.WriteLine($"Unit № {i}");
                Console.WriteLine($"Count: {Arm[i].BattleUS.count}");
                Console.WriteLine($"Name: {Arm[i].BattleUS.name}");
                Console.WriteLine($"HP: {Arm[i].BattleUS.hp}");
                Console.WriteLine($"Attack: {Arm[i].BattleUS.attack}");
                Console.WriteLine($"Damage: {Arm[i].BattleUS.damage}");
                Console.WriteLine($"Defence: {Arm[i].BattleUS.defence}");
                Console.WriteLine();
            }
        }
    }
}
