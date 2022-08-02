using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_v2
{
    public class BattleArmy
    {
        private List<BattleUnitsStack> Arm = new List<BattleUnitsStack>();

        public IList<BattleUnitsStack> arm { get { return Arm.ToArray(); } }

        public BattleArmy()
        { }

        public BattleArmy(Army x)
        {
            for (int i = 0; i < x.arm.Count; i++)
                Arm.Add(new BattleUnitsStack(x.arm[i]));
        }

        public BattleArmy(BattleArmy x)
        {
            for (int i = 0; i < x.arm.Count; i++)
                Arm.Add(new BattleUnitsStack(x.arm[i]));
        }

        public bool Check_Full()
        {
            if (Arm.Count == 9)
                return true;
            else
                return false;
        }

        public void Add(BattleUnitsStack x)
        {
            if (Arm.Count < 9)
                Arm.Add(x);
            else
                Console.WriteLine("Army is full");
        }

        public void Del(int index)
        {
            Arm.RemoveAt(index);
        }

        public void Show()
        {
            for (int i = 0; i < Arm.Count; i++)
            {
                Console.WriteLine($"Unit № {i}");
                Console.WriteLine($"Count: {Arm[i].count}");
                Console.WriteLine($"Name: {Arm[i].name}");
                Console.WriteLine($"HP: {Arm[i].hp}");
                Console.WriteLine($"Attack: {Arm[i].attack}");
                Console.WriteLine($"Damage: {Arm[i].damage}");
                Console.WriteLine($"Defence: {Arm[i].defence}");
                Console.WriteLine($"Initiative: {Arm[i].init}");

                if (Arm[i].effects.Count != 0)
                {
                    Console.WriteLine("Список наложенных эффектов:");
                    Arm[i].Get_Effects();
                }
                    
                Console.WriteLine("");
            }
        }
    }
}
