using System.Collections.Generic;

namespace Lab2 {
    public class Marine : Unit {
        public Marine() : base("Marine", 10, 4, 2, 2, 10) {
        }
    }

    public class Marauder : Unit {
        public Marauder() : base("Marauder", 25, 4, 4, 1, 15) {
        }
    }

    public class Medivac : Unit {
        public Medivac() : base("Medivac", 30, 0, 1, 0, 5) {
        }
    }

    public class Tank : Unit {
        public Tank() : base("Tank", 40, 10, 1, 5, 8) {
        }
    }

    public class Ghost : Unit {
        public Ghost() : base("Ghost", 17, 4, 3, 4, 13) {
            PAbilities.Add("EMP");
            PAbilities.Add("Snipe Shot");
        }
    }
}