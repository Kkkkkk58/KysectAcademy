using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class BoneDragon : Unit
{
    public BoneDragon() : base("BONEDRAGON", 150, 27, 28, 15, 30, 11,1)
    {
        SpellType = (SpellTypes)System.Enum.Parse(typeof(SpellTypes), "UNIT");
        Tribe = "undead";
    }
    public override bool Spell(BattleUnitStack me, BattleUnitStack target)
    {
        if ((me.Side == target.Side) && (me.ID != target.ID))
        {
            target.changeAttack(-12);
            return true;
        }
        else
        {
            return false;
        }
    }
}