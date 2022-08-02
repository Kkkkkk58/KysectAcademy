using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_v2
{
    public class Global
    {
        static private int Index = 0;
        static public int index
        {
            get
            {
                Index++;
                return Index - 1;
            }
        }
    }

    public class Global_Unit
    {
        static public List<Unit> Lists = new List<Unit>();
        
        static Global_Unit()
        {
            Lists.Add(new Angel());
            Lists.Add(new Arbalester());
            Lists.Add(new BoneDragon());
            Lists.Add(new Cyclop());
            Lists.Add(new Devil());
            Lists.Add(new Fury());
            Lists.Add(new Griffin());
            Lists.Add(new Hydra());
            Lists.Add(new Lich());
            Lists.Add(new Shaman());
            Lists.Add(new Skeleton());
            Lists.Add(new Spirit());

            ModsMan.GetUnits();
        }

        static public void Show()
        {
            for (int i = 0; i < Lists.Count; i++)
                Console.WriteLine($"{i}. {Lists[i].name}");
        }
    }

}
