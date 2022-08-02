using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Cyclope : Unit
{
    public Cyclope() : base("CYCLOPE", 85, 20, 15, 18, 26, 10, 0)
    {
        Description = "This is the mod character!";
    }

    public override void PassiveEffect(Battle curGame, int ID)
    {

        curGame.effect.Add(ID, "ACT", "EnemyNoCounter", 9999);
        curGame.effect.Add(ID, "ACTED", "NoCounter", 9999);

    }
}