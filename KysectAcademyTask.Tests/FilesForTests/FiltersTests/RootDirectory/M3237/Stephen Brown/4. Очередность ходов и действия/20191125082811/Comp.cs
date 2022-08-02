using System;
using System.Collections.Generic;
using System.Text;

namespace Laba
{
    public class Comp: IComparer<(BattleUnitsStack, int, int, Activ, Cntr)>
    {
        public int Compare((BattleUnitsStack, int, int, Activ, Cntr) x1, (BattleUnitsStack, int, int, Activ, Cntr) x2)
        {
            if (x1.Item1.init > x2.Item1.init)
                return -1;
            else
            if (x1.Item1.init < x2.Item1.init)
                return 1;
            else
            {
                Random rnd = new Random();
                int z = rnd.Next(1, 2);
                if (z == 2)
                    return -1;
                else
                    return 1;
            }
        }
    }
}
