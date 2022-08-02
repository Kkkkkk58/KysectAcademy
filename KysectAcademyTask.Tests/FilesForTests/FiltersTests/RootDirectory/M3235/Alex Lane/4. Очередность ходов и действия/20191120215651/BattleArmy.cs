using System;
using System.Collections.Generic;

namespace Lab2 {
    public class BattleArmy {
        private readonly List<BattleUnitStack> _battleUnitStack = new List<BattleUnitStack>();

        public List<BattleUnitStack> PBattleUnitStack {
            get => _battleUnitStack;
        }

        public BattleArmy(Army army) {
            foreach (var variable in  army.PArmyStack) {
                AddStack(new BattleUnitStack(variable));
            }
        }

        public void AddStack(BattleUnitStack unitStack) {
            if(unitStack.PCount >= 9) throw new Exception("Battle army must contain < 10 stacks");
            _battleUnitStack.Add(unitStack);
        }

        public bool Alive() {
            return _battleUnitStack.Count > 0;
        }
    }
}