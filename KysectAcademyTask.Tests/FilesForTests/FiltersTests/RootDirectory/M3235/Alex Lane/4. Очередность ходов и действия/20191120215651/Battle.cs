using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2 {
    public class Battle {
        private readonly BattleArmy[] _army = new BattleArmy[2];
        private int _curTurn = 0;
        private List<BattleUnitStack> _turnOrderAttack = new List<BattleUnitStack> ();
        private List<BattleUnitStack> _turnOrderDefend = new List<BattleUnitStack> ();
        public int PCurTurn {
            get => _curTurn;
            set {
                if (value != 0 || value != 1) throw new Exception("Turn is only 0 or 1");
                _curTurn = value;
            }
        }
        
        //func that initialize battle
        public Battle(BattleArmy army0, BattleArmy army1) {
            _army[0] = army0;
            _army[1] = army1;
            Round(_army);
        }

        private void InitTurnOrder() {
            foreach (var VARIABLE in _army[_curTurn].PBattleUnitStack) {
                _turnOrderAttack.Add(VARIABLE);
            }

            foreach (var VARIABLE in _army[(_curTurn + 1) % 2].PBattleUnitStack) {
                _turnOrderDefend.Add(VARIABLE);
            }

            _turnOrderAttack.Sort();
            _turnOrderDefend.Sort();
        }

        private void KillUnits(BattleUnitStack stack, double damage) {
            stack.PCount -= Convert.ToInt32(Math.Floor(damage / stack.PHp));

            //and deal some damage, that didnt killed unit
            stack.PHp -= Convert.ToInt32(damage - stack.PHp * Math.Floor(damage / stack.PHp));
        }

        public void Turn(BattleUnitStack attacking, BattleUnitStack defending, string option) {
            if (option == "Attack") {
                Attack(attacking, defending);
            }

            if (option == "Snipe Shot") {
                SnipeShot(attacking, defending);
            }

            if (option == "Defend") {
                Defend(attacking);
            }
        }

        public void Attack(BattleUnitStack attacking, BattleUnitStack defending) {
            Random rand = new Random();
            double sumDamage;
            if (defending.PDefence >= attacking.PAttack) {
                sumDamage = attacking.PCount * attacking.PDmg * (1 + 0.05 * (attacking.PAttack - defending.PDefence));
            }
            else {
                sumDamage = attacking.PCount * attacking.PDmg / (1 + 0.05 * (defending.PDefence - attacking.PAttack));
            }

            //kill some units, according sumDamage
            sumDamage = rand.NextDouble() * (attacking.PRandomDeviation) + sumDamage - attacking.PRandomDeviation;
            KillUnits(defending, sumDamage);
        }

        private void SnipeShot(BattleUnitStack attacking, BattleUnitStack defending) {
            //Snipe shot is a skill, that deal huge, but fixed damage to one enemy. The ghost stack make snipe shots to enemy stack in a order, while each enemy unit in chosen stack alive
            double snipeShotDamage = 40;
            double sumDamage = attacking.PCount * snipeShotDamage;
            KillUnits(defending, sumDamage);
        }

        private void Defend(BattleUnitStack attacking) {
            attacking.PDefence = Convert.ToInt32(attacking.PDefence * 1.3);
        }

        private void Wait(BattleUnitStack attacking, List<BattleUnitStack> turnOrder) {
            turnOrder.Insert(0, attacking);
        }

        private void IncInit(BattleUnitStack attacking, float addInit, List<BattleUnitStack> turnOrder) {
            for(int i = 0; i < turnOrder.Count; i++) {
                if (turnOrder[i] == attacking) {
                    turnOrder[i].PInitiative += addInit;
                    for (int j = i + 1; j < turnOrder.Count; j++) {
                        if (turnOrder[j].PInitiative > turnOrder[i].PInitiative) {
                            //find, where init becomes greater and replace BUStack
                            BattleUnitStack tmp = turnOrder[i];
                            turnOrder.RemoveAt(i);
                            turnOrder.Insert(j - 1, tmp);
                        }
                    }
                }
            }
        }
        
        //function to ask for BattleUnitStack
        public BattleUnitStack GetBattleUnitStack(BattleArmy army) {
            
        }
        
        //function to ask for option for chosen AttBUStack (if they are available)
        public string GetOption(BattleUnitStack bustack) {
            foreach (var VARIABLE in bustack.PAbilities) {
                //Write availoble abilities
            }
            
        }

        public void Round(BattleArmy [] army) {
            string curOption = "";
            InitTurnOrder();
            
            while (_army[(_curTurn + 1) % 2].Alive() || curOption != "Surrender") {
                //select AttStack and remove it
                if (_curTurn == 0) {
                    BattleUnitStack attBUStack = _turnOrderAttack[_turnOrderAttack.Count - 1];
                    _turnOrderAttack.RemoveAt(_turnOrderAttack.Count - 1);
                        
                    BattleUnitStack defBUStack = GetBattleUnitStack(army[(_curTurn + 1) % 2]); //should return chosen BattleUnitStack to attack
                    
                    curOption = GetOption(attBUStack); //should return chosen option for AttBUStack
                    
                    Turn(attBUStack, defBUStack, curOption);
                }

                if (_curTurn == 1) {
                    BattleUnitStack attBUStack = _turnOrderDefend[_turnOrderDefend.Count - 1];
                    _turnOrderDefend.RemoveAt(_turnOrderDefend.Count - 1);
                    
                    BattleUnitStack defBUStack = GetBattleUnitStack(army[(_curTurn + 1) % 2]); //should return chosen BattleUnitStack to attack
                    
                    curOption = GetOption(attBUStack); //should return chosen option for AttBUStack
                    
                    Turn(attBUStack, defBUStack, curOption);
                }
                
                _curTurn = (_curTurn + 1) % 2;
            }
            //Army can not die during its turn;
            //Army[CurTurn] - winner;
            
        }
        
    }
}