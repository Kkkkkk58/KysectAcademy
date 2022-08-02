using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EffectTool
{
    Random random = new Random(); //randomizer

    public class Effect
    {
        public int ID { get; private set; }
        public enum ConditionIDs
        {
            BEGIN, //begin of battle
            TURN, //begin of new turn
            SELECT, //stack selected
            ACT, //stack acted
            ACTED,
            AFFECT,
            AFFECTED,//stack got touched by other stack
            END //end of battle
        }
        public ConditionIDs ConditionID { get; private set; }
        public string EffectName { get; private set; }
        public int Timer;

        public Effect(int curID, string curCondition,string curName, int curTimer)
        {
            ID = curID;
            ConditionID = (ConditionIDs)System.Enum.Parse(typeof(ConditionIDs), curCondition);
            EffectName = curName;
            Timer = curTimer;
        }
    }
    List<BattleUnitStack> Queue;
    List<Effect> EffectStack;
    Battle battle;

    public bool restoreCounter = false; //if something changed because of effects, but need to be restored, make flag
    public bool restoreAttack = false;
    public bool restoreDefence = false;
    public bool replacedAttack = false;

    public bool Find(Effect x)
    {
        if (x.Timer == 0)
        {
            return true;
        }
        return false;
    }

    public EffectTool(List<BattleUnitStack> effectedArmy, Battle curbattle)
    {
        EffectStack = new List<Effect>();
        battle = curbattle;
        Queue = effectedArmy;
    }

    public void Add(int curID, string curCondition, string curName, int curTimer)
    {
        EffectStack.Add(new Effect(curID, curCondition, curName, curTimer));
    }

    public void Delete()
    {
        EffectStack.RemoveAll(Find);
    }

    public void TimerDown()
    {
        foreach(Effect u in EffectStack)
        {
            u.Timer = u.Timer - 1;
            if (u.Timer < 0)
            {
                u.Timer = 0;
            }
        }
        Delete();
    }

    public void Act(int IDtarget, int curCondition)
    {
        Log log = Log.getInstance();
        BattleUnitStack target = null;
        foreach(BattleUnitStack u in Queue)
        {
            if (IDtarget == u.ID)
            {
                target = u;
            }
        }
        foreach (Effect u in EffectStack)
        {

            if ((u.ID == IDtarget) && (curCondition == (int)u.ConditionID))
            {
                if (u.EffectName == "resistence")
                {
                    target.setCounter(1);
                }
                if (u.EffectName == "NoCounter")
                {
                    restoreCounter = true;
                    target.setCounter(0);
                }
                if (u.EffectName == "Markshooter")
                {
                    restoreDefence = true;
                    battle.ArmyQueue.Find(battle.Find).changeDefence(-battle.ArmyQueue.Find(battle.Find).curDefence);
                }
                if (u.EffectName == "EnemyNoCounter")
                {
                    restoreCounter = true;
                    battle.ArmyQueue.Find(battle.Find).setCounter(0);
                }
                if (u.EffectName == "AOE")
                {
                    log.sb.AppendLine("It's AOE damage!");
                    replacedAttack = true;
                    
                    /////////////////////////////////////////////////////////
                    foreach (BattleUnitStack stack in battle.ArmyQueue)
                    {
                        if (stack.Side != battle.curStack.Side)
                        {
                            double defence = battle.curStack.curDefence;
                            double Alive = (double)battle.curStack.curHitPoints / battle.curStack.minion.HitPoints;
                            Alive = Math.Ceiling(Alive);
                            int minDamage;
                            int maxDamage;
                            int damage;
                            if (stack.isDefend == 1)
                            {
                                defence = Math.Ceiling(stack.curDefence * 1.3);
                            }
                            if (battle.curStack.curAttack >= defence)
                            {
                                minDamage = Convert.ToInt32(Alive * battle.curStack.minion.MinDamage * (1 + 0.05 * (battle.curStack.curAttack - defence)));
                                maxDamage = Convert.ToInt32(Alive * battle.curStack.minion.MaxDamage * (1 + 0.05 * (battle.curStack.curAttack - defence)));
                                damage = random.Next(minDamage, maxDamage);
                            }
                            else
                            {
                                minDamage = Convert.ToInt32(Alive * battle.curStack.minion.MinDamage * (1 + 0.05 / (defence - battle.curStack.curAttack)));
                                maxDamage = Convert.ToInt32(Alive * battle.curStack.minion.MaxDamage * (1 + 0.05 / (defence - battle.curStack.curAttack)));
                                damage = random.Next(minDamage, maxDamage);
                            }
                            stack.changeHealth(-damage);
                        }
                    }
                    }
                    /////////////////////////////////////////////////////////
                }

            }
        }
    }
