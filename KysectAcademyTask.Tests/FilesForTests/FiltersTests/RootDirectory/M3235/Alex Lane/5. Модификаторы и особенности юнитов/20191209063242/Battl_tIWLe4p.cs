namespace Lab2 {
    public class BattleUnitStack {

        public BattleUnitStack(UnitStack us) {
            Type = us.Type;
            
        }
        
        private Unit Type;
        public Unit pType {
            get => Type;
        }

        private int Count;
        public int pCount {
            get => Count;
            set {
                if(value < 0) throw new Exception("Count must be > 0");
                else if (value > 999999) throw new Exception("Count can not be more than 999999");
                else Count = value;
            }
        }

        private int HP;
        public int pCurHP {
            get => CurHP;
            set {
                if (value < 0) throw new Exception("Hp must be >= 0");
                else CurHP = value;
            }
        }

        private int Attack;
        public int pAttack {
            get => Attack;
            set {
                if (value < 0) throw new Exception("Attack must be >= 0");
                else Attack = value;
            }
        }

        private int Defence;
        public int pDefence {
            get => Defence;
            set {
                if (value < 0) throw new Exception("Defence must be >= 0");
                else Defence = value;
            }
        }

        private int RandomDeviation;
        public int pRandomDeviation {
            get => RandomDeviation;
            set {
                if (value < 0) throw new Exception("Random deviation must be >= 0");
                else RandomDeviation = value;
            }
        }
        
        private int Dmg;
        public int pDmg {
            get => Dmg;
            set {
                if (value < 0) throw new Exception("Damage must be >= 0");
                else Dmg = value;
            }
        }

        private int Initiative;
        public int pInitiative {
            get => Initiative;
            set {
                if (value < 0) throw new Exception("Initiative must be >= 0");
                else Initiative = value;
            }
        }
    }
}