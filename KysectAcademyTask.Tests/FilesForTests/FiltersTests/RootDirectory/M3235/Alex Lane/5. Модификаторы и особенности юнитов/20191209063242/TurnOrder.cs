using System;
using System.Collections.Generic;

namespace Lab2 {
    public class TurnOrder {
        private static TurnOrder _instance;

        private TurnOrder() { }

        public static TurnOrder GetInstance()
        {
            if (_instance == null)
                _instance = new TurnOrder();
            return _instance;
        }
        
        private List<BattleUnitStack> _turnOrder = new List<BattleUnitStack> ();
        private List<BattleUnitStack> _waiting = new List<BattleUnitStack>();
        
        public TurnOrder (BattleArmy[] army) {
            foreach (var battleUnitStack in army[0].PBattleArmy) {
                _turnOrder.Add(battleUnitStack);
            }

            foreach (var battleUnitStack in army[1].PBattleArmy) {
                _turnOrder.Add(battleUnitStack);
            }
            
            _turnOrder.Sort(delegate(BattleUnitStack st1, BattleUnitStack st2) {
                return st1.PInitiative.CompareTo(st2.PInitiative);
            });
        }
        
        public void ShowTurnOrder() {
            Console.WriteLine("\n----------TURN ORDER---------");
            foreach (var stack in _waiting) {
                Console.WriteLine(" --TEAM-- " + stack.PTeam + " --TYPE-- " + stack.PType.GetStringType() + " -COUNT- " + stack.PCount);
            }
            foreach (var stack in _turnOrder) {
                Console.WriteLine(" --TEAM-- " + stack.PTeam + " --TYPE-- " + stack.PType.GetStringType() + " -COUNT- " + stack.PCount);
            }
            Console.WriteLine();
        }

        public bool IsEmpty() {
            return _turnOrder.Count == 0 && _waiting.Count == 0;
        }
        
        //temporarily increase init
        public void TmpIncInit(BattleUnitStack attacking, float addInit) {
            for (int i = 0; i < _turnOrder.Count; i++) {
                if (_turnOrder[i] == attacking) {
                    double tempInit = _turnOrder[i].PInitiative + addInit;
                    for (int j = i + 1; j < _turnOrder.Count; j++) {
                        if (_turnOrder[j].PInitiative > tempInit) {
                            //find, where init becomes greater and replace BUStack
                            BattleUnitStack tmp = _turnOrder[i];
                            _turnOrder.RemoveAt(i);
                            _turnOrder.Insert(j - 1, tmp);
                        }
                    }
                }
            }
        }

        public void Wait(BattleUnitStack attacking) {
            _waiting.Add(attacking);
        }
        
        public BattleUnitStack GetAttBattleUnitStack() {
            if (_turnOrder.Count == 0) return _waiting[_waiting.Count - 1];
            return _turnOrder[_turnOrder.Count - 1];
        }

        public void Remove(BattleUnitStack st) {
            _turnOrder.Remove(st);
            _waiting.Remove(st);
        }
    }
}