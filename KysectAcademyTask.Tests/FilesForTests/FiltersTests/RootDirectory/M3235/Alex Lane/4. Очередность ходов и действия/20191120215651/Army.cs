using System;
using System.Collections.Generic;

namespace Lab2 {
    public class Army {
        private readonly List<UnitStack> _armyStack = new List<UnitStack>();

        public List<UnitStack> PArmyStack {
            get => _armyStack;
        }

        //private UnitStack[] ArmyStack = new UnitStack[6];

        public Army(List<UnitStack> unSt) {
            _armyStack = unSt;
        }

        public Army() {
        }

        public void AddStack(UnitStack st) {
            if (_armyStack.Count > 5) {
                throw new Exception("Army if full");
            }
            _armyStack.Add(st);
        }

        public void DeleteStack(int i) {
            if (i < 0 || i > _armyStack.Count) {
                throw new IndexOutOfRangeException("Wrong index");
            }
            _armyStack.RemoveAt(i);
        }

        public void ShowArmy() {
            
        }
    }
}