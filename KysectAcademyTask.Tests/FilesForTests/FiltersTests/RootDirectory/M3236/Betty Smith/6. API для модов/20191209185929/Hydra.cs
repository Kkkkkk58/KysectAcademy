using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Hydra : Unit
{
    public Hydra() : base("HYDRA", 80, 15, 12, 7, 14, 7,0)
    {
    }
    public override void PassiveEffect(Battle curGame, int ID)
    {

        curGame.effect.Add(ID, "ACT", "AOE", 9999);

    }

}