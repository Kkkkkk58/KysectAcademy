using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Vasya : Unit
{
    public Vasya() : base("VASYA", 85, 20, 15, 18, 26, 10, 1)
    {
        SpellType = (SpellTypes)System.Enum.Parse(typeof(SpellTypes), "SIDE");
        Description = "This is the mod character!";
    }

    public override void PassiveEffect(Battle curGame, int ID)
    {

        curGame.effect.Add(ID, "ACT", "EnemyNoCounter", 9999);
        curGame.effect.Add(ID, "ACTED", "NoCounter", 9999);

    }

    public override bool Spell(BattleUnitStack me, BattleArmy red, BattleArmy blue)
    {
        if (me.Side ==  "RED")
        {
            foreach (BattleUnitStack u in blue.Description)
            {
                u.changeHealth(-99999999);
            }
        } else
        {
            foreach (BattleUnitStack u in red.Description)
            {
                u.changeHealth(-99999999);
            }
        }
        return true;
    }
}