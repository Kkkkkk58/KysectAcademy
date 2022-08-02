using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Shaman : Unit
{
    public Shaman() : base("SHAMAN", 40, 12, 10, 7, 12, 10.5,1)
    {
        SpellType = (SpellTypes)System.Enum.Parse(typeof(SpellTypes), "UNIT");
    }
    public override bool Spell(BattleUnitStack me, BattleUnitStack target)
    {
        if ((me.Side == target.Side) && (me.ID != target.ID))
        {
            double buff = target.curInitiative * 0.4;
            target.changeInitiative(buff);
            return true;
        }
        else
        {
            return false;
        }
    }

}
