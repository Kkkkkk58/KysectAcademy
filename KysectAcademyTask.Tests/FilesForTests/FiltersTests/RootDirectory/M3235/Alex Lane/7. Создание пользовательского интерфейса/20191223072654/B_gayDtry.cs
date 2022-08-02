using System;
using System.Collections.Generic;

namespace Lab2 {
    public class BattleArmy {
        private readonly List<BattleUnitStack> _battleArmy = new List<BattleUnitStack>();
        
        private int _team;
        public int PTeam {
            get => _team;
            set {
                if (value != 0 && value != 1) throw new Exception("Team value is only 0 or 1");
                foreach (BattleUnitStack BUSt in _battleArmy) {
                    BUSt.PTeam = value;
                }
                _team = value;
            }
        }
        
        public List<BattleUnitStack> PBattleArmy {
            get => _battleArmy;
        }

        public BattleArmy(Army army) {
            foreach (var variable in  army.PArmyStack) {
                AddStack(new BattleUnitStack(variable));
            }
        }

        public void AddStack(BattleUnitStack unitStack) {
            if(_battleArmy.Count >= 9) throw new Exception("Battle army must contain < 10 stacks");
            _battleArmy.Add(unitStack);
        }

        public bool Alive() {
            return _battleArmy.Count > 0;
        }
    }
}