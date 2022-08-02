using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Fury : Unit
{
    public Fury() : base("FURY", 16, 5, 3, 5, 7, 16,0)
    {

    }

    public override void PassiveEffect(Battle curGame, int ID)
    {

        curGame.effect.Add(ID, "ACT", "EnemyNoCounter", 9999);

    }
}
