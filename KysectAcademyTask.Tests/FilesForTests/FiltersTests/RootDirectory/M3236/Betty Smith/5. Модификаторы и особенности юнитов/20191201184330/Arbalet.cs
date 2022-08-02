using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Arbalet : Unit
    {
        public Arbalet() : base("ARBALET", 10, 4, 4, 2, 8, 8, 0)
        {
            Description = "ARBALET";
        }
    public override void PassiveEffect(Battle curGame,int myID)
    {
        curGame.effect.Add(myID, "BEGIN", "work", 9999);
    }


}

