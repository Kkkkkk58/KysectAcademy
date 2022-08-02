using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Devil : Unit
{
    public Devil() : base("DEVIL", 166, 27, 25, 36, 66, 11,1)
    {
        SpellType = (SpellTypes)System.Enum.Parse(typeof(SpellTypes), "UNIT");

    }
    public override bool Spell(BattleUnitStack me, BattleUnitStack target)
    {
        if ((me.Side != target.Side) && (me.ID != target.ID))
        {
            target.changeDefence(-12);
            return true;
        }
        else
        {
            return false;
        }
    }
}