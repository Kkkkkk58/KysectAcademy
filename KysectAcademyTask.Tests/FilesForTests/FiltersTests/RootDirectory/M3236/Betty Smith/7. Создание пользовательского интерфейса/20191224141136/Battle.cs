using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Battle
{
    Writer writer = new Writer(); //operrator
    Random random = new Random(); //randomizer
    public EffectTool effect;
    public BattleArmy BLUE;
    public BattleArmy RED;
    public ArmyClass SaveBLUE;
    public ArmyClass SaveRED; //Used after battle to change sides CrusadeArmy

    public int HypercurAttack;
    public int HypercurDefence;
    public int Hypercounter;


    public List<BattleUnitStack> ArmyQueue = new List<BattleUnitStack>();
    public BattleUnitStack curStack;

    private int setID = 0; // grow each time new stack summoned

    public int Compare(BattleUnitStack x, BattleUnitStack y)
    {
        // CompareTo() method 
        return y.queueInitiative.CompareTo(x.queueInitiative);

    }
    public bool First(BattleUnitStack x)
    {
        if (x.queueInitiative == -900)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    int findID = 0;
    public bool Find(BattleUnitStack x)
    {
        if (x.ID == findID)
        {
            return true;
        }
        return false;
    }

    public Battle(ArmyClass initBLUE, ArmyClass initRED)
    {
        Log log = Log.getInstance();
        log.sb.AppendLine($"Begin construction of battle");
        BLUE = new BattleArmy(initBLUE);
        foreach (BattleUnitStack u in BLUE.Description)
        {
            u.setID(setID);
            setID = setID + 1;
            u.setSide("BLUE");
            u.setCounter(1);
            u.setIsWaited(0);
            u.setSpell(u.minion.HasSpell);
        }
        log.sb.AppendLine($"Blue army created");

        RED = new BattleArmy(initRED);
        foreach (BattleUnitStack u in RED.Description)
        {
            u.setID(setID);
            setID = setID + 1;
            u.setSide("RED");
            u.setCounter(1);
            u.setIsWaited(0);
            u.setSpell(u.minion.HasSpell);
        }
        log.sb.AppendLine($"Red army created");
        SaveBLUE = initBLUE;
        SaveRED = initRED;
        ArmyQueue.AddRange(BLUE.Description);
        ArmyQueue.AddRange(RED.Description);
        ArmyQueue.Sort(Compare);
        effect = new EffectTool(ArmyQueue,this);
        log.sb.AppendLine($"Start game loop");
        GameLoop();
        log.sb.AppendLine($"Change armies");
        ChangeArmies();
    }

    public void ShowInfo()
    {
        writer.MainInfo(ArmyQueue, BLUE, RED);
    }

    private bool isThereTurn()
    {
        return ArmyQueue.Exists(First);
    }

    private void Refresh()
    {
        ArmyQueue.Clear();
        ArmyQueue.AddRange(BLUE.Description);
        ArmyQueue.AddRange(RED.Description);
        ArmyQueue.Sort(Compare);
    }
    private void attack(BattleUnitStack attacker, BattleUnitStack defender)
    {
            double defence = defender.curDefence;
            double Alive = (double)attacker.curHitPoints / attacker.minion.HitPoints;
            Alive = Math.Ceiling(Alive);
            int minDamage;
            int maxDamage;
            int damage;
            if (defender.isDefend == 1)
            {
                defence = Math.Ceiling(defender.curDefence * 1.3);
            }
            if (attacker.curAttack >= defence)
            {
                minDamage = Convert.ToInt32(Alive * attacker.minion.MinDamage * (1 + 0.05 * (attacker.curAttack - defence)));
                maxDamage = Convert.ToInt32(Alive * attacker.minion.MaxDamage * (1 + 0.05 * (attacker.curAttack - defence)));
                damage = random.Next(minDamage, maxDamage);
            }
            else
            {
                minDamage = Convert.ToInt32(Alive * attacker.minion.MinDamage * (1 + 0.05 / (defence - attacker.curAttack)));
                maxDamage = Convert.ToInt32(Alive * attacker.minion.MaxDamage * (1 + 0.05 / (defence - attacker.curAttack)));
                damage = random.Next(minDamage, maxDamage);
            }
            defender.changeHealth(-damage);
            if ((defender.curHitPoints != 0) && (defender.counter != 0)) //counter
            {
            Log log = Log.getInstance();
            log.sb.AppendLine($"{defender.ID}:{defender.minion.Type} counter attack");
            Alive = (double)defender.curHitPoints / defender.minion.HitPoints;
                Alive = Math.Ceiling(Alive);
                if (defender.curAttack >= attacker.curDefence)
                {
                    minDamage = Convert.ToInt32(Alive * defender.minion.MinDamage * (1 + 0.05 * (defender.curAttack - defence)));
                    maxDamage = Convert.ToInt32(Alive * defender.minion.MaxDamage * (1 + 0.05 * (defender.curAttack - defence)));
                    damage = random.Next(minDamage, maxDamage);
                }
                else
                {
                    minDamage = Convert.ToInt32(Alive * defender.minion.MinDamage * (1 + 0.05 / (defence - defender.curAttack)));
                    maxDamage = Convert.ToInt32(Alive * defender.minion.MaxDamage * (1 + 0.05 / (defence - defender.curAttack)));
                    damage = random.Next(minDamage, maxDamage);
                }
                attacker.changeHealth(-damage);
                defender.setCounter(0);
            }
    }

    public void GameLoop()
    {
        Log log = Log.getInstance();
        bool input;
        string action;
        string winner = "";
        //Add passives
        log.sb.AppendLine($"Apply passives");
        foreach (BattleUnitStack u in ArmyQueue)
        {
            u.minion.PassiveEffect(this,u.ID);
            effect.Act(u.ID, 0);    
        }
        log.sb.AppendLine($"BATTLE BEGIN");
        while (true)
        {
            effect.TimerDown();
            foreach (BattleUnitStack u in ArmyQueue)
            {
                effect.Act(u.ID, 1);
            }
            int reverse = -100;
            // Beggining of turn
            while (isThereTurn())
            {
                input = false;
                Refresh();
                writer.MainInfo(ArmyQueue, BLUE, RED);
                curStack = ArmyQueue.Find(First);
                effect.Act(curStack.ID, 2);
                // action holder
                writer.YourTurn(curStack);
                while (!input)
                {
                    action = Console.ReadLine();
                    switch (action)
                    {
                        case "1":
                            input = false;
                            writer.Target("enemy");
                            log.sb.AppendLine($"{curStack.ID}:{curStack.minion.Type} attack");
                            while (!input)
                            {
                                action = Console.ReadLine();
                                bool isNumeric = int.TryParse(action, out int n);
                                if (!isNumeric)
                                {
                                    continue;
                                }
                                findID = Int32.Parse(action);
                                if (!ArmyQueue.Exists(Find))
                                {
                                    continue;
                                }
                                if (ArmyQueue.Find(Find).Side == curStack.Side)
                                {
                                    continue;
                                }
                                input = true;
                            }

                            Hypercounter = ArmyQueue.Find(Find).counter;
                            HypercurAttack = ArmyQueue.Find(Find).curAttack;
                            HypercurDefence = ArmyQueue.Find(Find).curDefence;


                            effect.restoreCounter = false; //if something changed because of effects, but need to be restored, make flag
                            effect.restoreAttack = false;
                            effect.restoreDefence = false;
                            effect.replacedAttack = false;

                            effect.Act(curStack.ID,3);
                            effect.Act(ArmyQueue.Find(Find).ID,4);

                            if (effect.replacedAttack == false)
                            {
                                attack(curStack, ArmyQueue.Find(Find));
                            }

                            if (effect.restoreAttack == true)
                            {
                                ArmyQueue.Find(Find).changeAttack(HypercurAttack - ArmyQueue.Find(Find).curAttack);
                            }
                            if (effect.restoreCounter == true)
                            {
                                ArmyQueue.Find(Find).setCounter(Hypercounter);
                            }
                            if (effect.restoreDefence == true)
                            {
                                ArmyQueue.Find(Find).changeDefence(HypercurDefence - ArmyQueue.Find(Find).curDefence);
                            }

                            effect.Act(curStack.ID,5);
                            effect.Act(ArmyQueue.Find(Find).ID,6);
                            curStack.changeQueueInitiative(-900);
                            BLUE.RemoveDead();
                            RED.RemoveDead();
                            break;


                        case "2":
                            if (curStack.Spell == 0)
                            {
                                break;
                            }
                            input = true;
                            curStack.setSpell(0);
                            bool act = false;
                            log.sb.AppendLine($"{curStack.ID}:{curStack.minion.Type} use spell");
                            writer.Target("spell");
                            if ($"{curStack.minion.SpellType}" == "UNIT")
                            {
                                while (act != true)
                                {
                                    input = false;
                                    while (!input)
                                    {
                                        action = Console.ReadLine();
                                        if (action == "quit")
                                        {
                                            break;
                                        }
                                        bool isNumeric = int.TryParse(action, out int n);
                                        if (!isNumeric)
                                        {
                                            continue;
                                        }
                                        findID = Int32.Parse(action);
                                        if (!ArmyQueue.Exists(Find))
                                        {
                                            continue;
                                        }
                                        input = true;
                                    }
                                    if (action == "quit")
                                    {
                                        log.sb.AppendLine($"{curStack.ID}:{curStack.minion.Type} actually not using a spell and just quit");
                                        curStack.changeQueueInitiative(-900);
                                        BLUE.RemoveDead();
                                        RED.RemoveDead();
                                        break;
                                    }
                                    act = curStack.minion.Spell(curStack, ArmyQueue.Find(Find));
                                }
                            }
                            else if ($"{curStack.minion.SpellType}" == "SIDE")
                            {
                                curStack.minion.Spell(curStack, RED, BLUE);
                            }
                            else if ($"{curStack.minion.SpellType}" == "ALL")
                            {
                                curStack.minion.Spell(curStack, ArmyQueue);
                            }
                            else
                            { throw new ArgumentException("INVALID UNIT DATA: WRONG SpellType"); }
                            curStack.changeQueueInitiative(-900);
                            BLUE.RemoveDead();
                            RED.RemoveDead();
                            break;
                        case "3":
                            if (curStack.isWaited == 1)
                            {
                                break;
                            }
                            curStack.setIsWaited(1);
                            input = true;
                            curStack.changeQueueInitiative(reverse);
                            log.sb.AppendLine($"{curStack.ID}:{curStack.minion.Type} is waiting");
                            reverse = reverse + 1;
                            break;
                        case "4":
                            log.sb.AppendLine($"{curStack.ID}:{curStack.minion.Type} is defending");
                            curStack.setIsDefend(1);
                            curStack.changeQueueInitiative(-900);
                            input = true;
                            break;
                        case "5":
                            input = true;
                            if (curStack.Side == "RED")
                            {
                                log.sb.AppendLine("RED GIVE UP");
                                winner = "BLUE";
                            }
                            else
                            {
                                log.sb.AppendLine("BLUE GIVE UP");
                                winner = "RED";
                            }
                            break;
                        case "6":
                            writer.Target("any");
                            while (!input)
                            {
                                action = Console.ReadLine();
                                bool isNumeric = int.TryParse(action, out int n);
                                if (!isNumeric)
                                {
                                    continue;
                                }
                                findID = Int32.Parse(action);
                                if (!ArmyQueue.Exists(Find))
                                {
                                    continue;
                                }
                                input = true;
                            }
                            writer.MainInfo(ArmyQueue, BLUE, RED);
                            writer.YourTurn(curStack);
                            writer.Show(ArmyQueue.Find(Find));
                            input = false;
                            break;
                        case "7":
                            writer.ShowLog();
                            writer.MainInfo(ArmyQueue, BLUE, RED);
                            writer.YourTurn(curStack);
                            input = false;
                            break;
                        default:
                            break;
                    }
                }
                    if (RED.IsDefeated())
                    {
                        winner = "BLUE";
                        Refresh();
                        break;
                    }
                    if (BLUE.IsDefeated())
                    {
                        winner = "RED";
                        Refresh();
                        break;
                    }
                    if (winner != "")
                {
                    break;
                }
                Refresh();
                    //
                }
                ArmyQueue.Sort(Compare);
            if (winner != "")
            {
                Console.WriteLine($"WINNER IS {winner}");
                log.sb.AppendLine($"WINNER IS {winner}");
                break;
            }

            log.sb.AppendLine("END OF TURN");
                // end of turn
                foreach (BattleUnitStack u in ArmyQueue)
                {
                    u.changeQueueInitiative(u.curInitiative);
                    u.setCounter(1);
                    u.setIsDefend(0);
                    u.setIsWaited(0);
                }

            }
        foreach (BattleUnitStack u in ArmyQueue)
        {
            effect.Act(u.ID, 7);
        }
        Refresh();
        return;
        }


        void ChangeArmies()
        {
        Log log = Log.getInstance();
        SaveBLUE.Description.Clear();
        foreach (BattleUnitStack u in BLUE.Description)
        {
            double Alive = (double)u.curHitPoints / u.minion.HitPoints;
            int add = (int)Math.Ceiling(Alive);
            string type = $"{u.minion.Type}";
            SaveBLUE.Add(type, add);
        }
        log.sb.AppendLine($"Blue army changed");
        SaveRED.Description.Clear();
        foreach (BattleUnitStack u in RED.Description)
        {
            double Alive = (double)u.curHitPoints / u.minion.HitPoints;
            int add = (int)Math.Ceiling(Alive);
            string type = $"{u.minion.Type}";
            SaveRED.Add(type, add);
        }
        log.sb.AppendLine($"Red army changed");

    }


    }
