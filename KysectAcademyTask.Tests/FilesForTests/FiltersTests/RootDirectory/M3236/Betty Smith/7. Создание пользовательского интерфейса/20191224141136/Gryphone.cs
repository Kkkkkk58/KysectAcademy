﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Gryphone : Unit
{
    public Gryphone() : base("GRYPHONE", 30, 7, 5, 5, 10, 15,0)
    {

    }
    public override void PassiveEffect(Battle curGame,int ID)
    {
        curGame.effect.Add(ID, "AFFECTED", "resistence", 9999);
    }
}