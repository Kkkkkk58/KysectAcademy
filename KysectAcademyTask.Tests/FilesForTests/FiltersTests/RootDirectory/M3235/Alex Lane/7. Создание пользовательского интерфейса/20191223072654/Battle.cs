using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2 {
    public class Battle {
        private readonly BattleArmy[] _army = new BattleArmy[2];

        private TurnOrder _turnOrder = TurnOrder.GetInstance();
        public Battle(BattleArmy army0, BattleArmy army1) {
            _army[0] = army0;
            _army[1] = army1;

            _army[0].PTeam = 0;
            _army[1].PTeam = 1;
            
            _turnOrder = new TurnOrder(_army);

            string option = "";
            while ((_army[0].Alive() && _army[1].Alive()) && option != "Surrender") {
                if (_turnOrder.IsEmpty()) _turnOrder = new TurnOrder(_army);
                option = Round(_army);
            }
        }

        private void KillUnits(BattleUnitStack stack, double damage) {
            Console.WriteLine("--------UNITS KILLED--------- " + Math.Min(Convert.ToInt32(Math.Floor(damage / stack.PHp)), stack.PCount));
            stack.PCount = stack.PCount - Convert.ToInt32(Math.Floor(damage / stack.PHp));
            if (stack.PCount == 0) {
                Console.WriteLine("--------STACK KILLED--------- " + stack.PType.GetStringType());
                _army[stack.PTeam].PBattleArmy.Remove(stack);
                _turnOrder.Remove(stack);
            }
        }

        public void Turn(BattleUnitStack attacking, BattleUnitStack defending, string option) {
            double dmgDuringTurn = 0;

            foreach (var ability in attacking.PAbilities) {
                if (option == ability.Key) {
                    try {
                        dmgDuringTurn += ability.Value.UseAbility(attacking, defending);
                    }
                    catch (Exception e) {
                        Console.WriteLine(e);
                    }
                }
            }
            
            if (option == "Defend") {
                try {
                    Defend(attacking);
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                }
                return;
            }

            if (option == "Wait") {
                try {
                    Wait(attacking);
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                }
                return;
            }

            Console.WriteLine("--------DAMAGE DEALED-------- " + dmgDuringTurn);

            if (defending != new BattleUnitStack()) {
                try {
                    KillUnits(defending, dmgDuringTurn);
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                }
            }
        }

        //this func can use to change characteristics of battleunitstack, except initiative
        private void Modify(BattleUnitStack stack, int hpChange, int defChange, int dmgChange, int attChange, int randDevChange) {
            stack.PHp += hpChange;
            stack.PDefence += defChange;
            stack.PDmg += dmgChange;
            stack.PAttack += attChange;
            stack.PRandomDeviation += randDevChange;
        }
        

        private void Defend(BattleUnitStack attacking) {
            attacking.PDefence = Convert.ToInt32(attacking.PDefence * 1.3);
        }

        private void Wait(BattleUnitStack attacking) {
            _turnOrder.Remove(attacking);
            _turnOrder.Wait(attacking);
        }

        public BattleUnitStack GetBattleUnitStack(BattleArmy army) {
            try {
                Console.WriteLine("Choose target Unit");
                for (int i = 0; i < army.PBattleArmy.Count(); i++) {
                    Console.WriteLine(i + " " + army.PBattleArmy[i].PType.GetStringType() + " " + army.PBattleArmy[i].PCount);
                }
                Console.WriteLine();

                int selectedBattleUnitStack = Convert.ToInt32(Console.ReadLine());
                return army.PBattleArmy[selectedBattleUnitStack];
            }
            catch {
                Console.WriteLine("WRONG OPTION when choosing BattleStack");
                throw;
            }
        }
        public string GetOption(BattleUnitStack bustack) {
            try {
                foreach (var ability in bustack.PAbilities) {
                    Console.WriteLine(ability.Key);
                }
                Console.WriteLine("Defend");
                Console.WriteLine("Wait");
                Console.WriteLine("Surrender");
                Console.WriteLine("Choose option\n");
                string selectedOption = Console.ReadLine();
                Console.WriteLine();
                return selectedOption;
            }
            catch {
                Console.WriteLine("WRONG OPTION when choosing option");
                throw;
            }
        }

        public string Round(BattleArmy [] army) {
            string curOption = "";
            try {
                
                _turnOrder.ShowTurnOrder();

                BattleUnitStack attBUStack = _turnOrder.GetAttBattleUnitStack(); //attacking BattleUnitStack

                Console.WriteLine("!!! ATTACKINGS STACK !!!" + attBUStack.PType.GetStringType() + " " +
                                  attBUStack.PCount + '\n');

                curOption = GetOption(attBUStack);
                BattleUnitStack defBUStack = new BattleUnitStack();

                if (curOption == "Revive") defBUStack = GetBattleUnitStack(_army[attBUStack.PTeam]);

                else if (curOption != "Defend" && curOption != "Wait")
                    defBUStack = GetBattleUnitStack(_army[(attBUStack.PTeam + 1) % 2]);

                Turn(attBUStack, defBUStack, curOption);

                if (curOption != "Wait") _turnOrder.Remove(attBUStack);

                Console.WriteLine("'\n'-------------------------------------------------------------------TURN IS OVER----------------------------------------------------------");
                if (!_army[0].Alive()) Console.WriteLine("TEAM 0 - WINNER");
                else if (!_army[1].Alive()) Console.WriteLine("TEAM 1 - WINNER");
                if (curOption == "Surrender") {
                    Console.WriteLine("Team " + (attBUStack.PTeam + 1) % 2 + " WINNER");
                }
            }
            catch {
                Console.WriteLine("SMTH GOES WRONG");
            }

            return curOption;
        }
    }
}