using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Lich : Unit
{
    public Lich() : base("LICH", 50, 15, 15, 12, 17, 10,1)
    {
        SpellType = (SpellTypes)System.Enum.Parse(typeof(SpellTypes), "UNIT");
        Tribe = "undead";
    }

    public override bool Spell(BattleUnitStack me, BattleUnitStack target)
    {
        if ((me.Side == target.Side) && (me.ID != target.ID))
        {
            if (target.minion.Tribe == "undead")
            {
                double Alive = (double)me.curHitPoints / me.minion.HitPoints;
                int casters = (int)Math.Ceiling(Alive);
                target.changeHealth(100 * casters);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void PassiveEffect(Battle curGame, int ID)
    {

        curGame.effect.Add(ID, "ACT", "EnemyNoCounter", 9999);
        curGame.effect.Add(ID, "ACTED", "NoCounter", 9999);

    }

}
