    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

class Angel : Unit
{
    public Angel() : base("ANGEL", 180, 27, 27, 45, 45, 11,1)
    {
        SpellType = (SpellTypes)System.Enum.Parse(typeof(SpellTypes), "UNIT");
        Description = "Holy wrath : change the attack of your friendly stack +12";
    }
    public override bool Spell(BattleUnitStack me, BattleUnitStack target)
    {
        if ((me.Side == target.Side) && (me.ID != target.ID))
        {
            target.changeAttack(12);
            return true;
        }
        else
        {
            return false;
        }
    }

}
