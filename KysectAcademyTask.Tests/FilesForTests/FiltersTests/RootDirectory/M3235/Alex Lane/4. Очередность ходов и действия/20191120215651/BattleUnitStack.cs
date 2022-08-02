using System;
using System.Collections.Generic;

namespace Lab2 {
    public class BattleUnitStack {

        public BattleUnitStack(UnitStack us) {
            _type = us.PType;
            _count = us.PCount;
            _hp = us.PType.PHp;
            _attack = us.PType.PAttack;
            _defence = us.PType.PDefence;
            _randomDeviation = us.PType.PRandomDeviation;
            _initiative = us.PType.PInitiative;
        }
        
        private readonly List<string> _abilities = new List<string>();
        public List<string> PAbilities {
            get => _abilities;
        }
        
        private Unit _type;
        public Unit PType => _type;

        private int _count;
        public int PCount {
            get => _count;
            set {
                if(value < 0) throw new Exception("Count must be > 0");
                if (value > 999999) throw new Exception("Count can not be more than 999999");
                _count = value;
            }
        }

        private int _hp;
        public int PHp {
            get => _hp;
            set {
                if (value < 0) throw new Exception("Hp must be >= 0");
                _hp = value;
            }
        }

        private int _attack;
        public int PAttack {
            get => _attack;
            set {
                if (value < 0) throw new Exception("Attack must be >= 0");
                _attack = value;
            }
        }

        private int _defence;
        public int PDefence {
            get => _defence;
            set {
                if (value < 0) throw new Exception("Defence must be >= 0");
                _defence = value;
            }
        }

        private int _randomDeviation;
        public int PRandomDeviation {
            get => _randomDeviation;
            set {
                if (value < 0) throw new Exception("Random deviation must be >= 0");
                _randomDeviation = value;
            }
        }
        
        private int _dmg;
        public int PDmg {
            get => _dmg;
            set {
                if (value < 0) throw new Exception("Damage must be >= 0");
                _dmg = value;
            }
        }

        private float _initiative;
        public float PInitiative {
            get => _initiative;
            set {
                if (value < 0) throw new Exception("Initiative must be >= 0");
                _initiative = value;
            }
        }
    }
}