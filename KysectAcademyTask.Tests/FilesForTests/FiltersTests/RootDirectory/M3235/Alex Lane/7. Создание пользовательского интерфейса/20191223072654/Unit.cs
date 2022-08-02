using System.Collections.Generic;

namespace Lab2 {
    public class Unit {

        private string _type;

        private int _attack,
            _defence,
            _hp,
            _randomDeviation;

        private double _dmg,
            _initiative;

        private Dictionary<string, Ability> _abilities = new Dictionary<string, Ability>();
        public Dictionary<string, Ability> PAbilities {
            get => _abilities;
            set { _abilities = value; }
        }

        public double PDmg {
            get => _dmg;
        }

        public int PHp {
            get => _hp;
            set {
                if (value < 0) _hp = 0;
                _hp = value;
            }
        }

        public int PAttack {
            get => _attack;
            set {
                if (value < 0) _attack = 0;
                _attack = value;
            }
        }

        public int PDefence {
            get => _defence;
            set { _defence = value; }
        }

        public int PRandomDeviation {
            get => _randomDeviation;
            set { _randomDeviation = value; }
        }

        public double PInitiative {
            get => _initiative;
        }

        public Unit(string type, int hp, int att, int def, int deviation, float init, double dmg) {
            _type = type;
            _dmg = dmg;
            _hp = hp;
            _attack = att;
            _defence = def;
            _randomDeviation = deviation;
            _initiative = init;
        }

        public string GetStringType() {
            return _type;
        }
    }
}