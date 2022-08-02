using System.Collections.Generic;

namespace Lab2 {
    public class Marine : Unit {
        public Marine() : base("Marine  ", 10, 4, 2, 2, 10, 4) {
            PAbilities.Add("Attack", new Attack());
        }
    }

    public class Marauder : Unit {
        public Marauder() : base("Marauder", 25, 10, 4, 1, 15, 3) {
            PAbilities.Add("Attack", new Attack());
        }
    }

    public class Medivac : Unit {
        public Medivac() : base("Medivac ", 30, 3, 1, 0, 5, 0) {
            PAbilities.Add("Revive", new Revive());
        }
    }

    public class Tank : Unit {
        public Tank() : base("Tank    ", 40, 10, 1, 5, 8, 40) {
            PAbilities.Add("Attack", new Attack());
        }
    }

    public class Ghost : Unit {
        public Ghost() : base("Ghost   ", 17, 10, 3, 4, 13, 25) {
            PAbilities.Add("Attack", new Attack());
            PAbilities.Add("Snipe Shot", new SnipeShot());
        }
    }
}