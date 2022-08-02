using System;
using System.Collections.Generic;

namespace Lab2 {
    public class UnitStack {

        private readonly Unit _type;
        private readonly int _count;

        private Dictionary<string, Ability> _abilities = new Dictionary<string, Ability>();
        public Dictionary<string, Ability> PAbilities {
            get => _abilities;
            set { _abilities = value; }
        }
        public Unit PType {
            get => _type;
        }

        public int PCount {
            get => _count;
        }

        public UnitStack(Unit type, int cnt) {
            PAbilities = type.PAbilities;
            _type = type;
            if (cnt <= 0 || cnt > 999999) {
                throw new Exception("Unit count should be > 0 and < 999999");
            }
            _count = cnt;
        }

        public UnitStack(UnitStack st) {
            PAbilities = st._abilities;
            _type = st._type;
            _count = st._count;
        }

        public new string GetType() {
            return _type.GetStringType();
        }

    }
}