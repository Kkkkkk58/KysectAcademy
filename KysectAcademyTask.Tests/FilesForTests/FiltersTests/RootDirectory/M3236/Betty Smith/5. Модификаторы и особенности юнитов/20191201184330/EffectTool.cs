using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class EffectTool
{
    public class Effect
    {
        public int ID { get; private set; }
        public enum ConditionIDs
        {
            BEGIN, //begin of battle
            TURN, //begin of new turn
            SELECT, //stack selected
            ACT, //stack acted
            ACTED, //stack got touched by other stack
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

    public bool Find(Effect x)
    {
        if (x.Timer == 0)
        {
            return true;
        }
        return false;
    }

    public EffectTool(List<BattleUnitStack> effectedArmy)
    {
        EffectStack = new List<Effect>();
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
        foreach (Effect u in EffectStack)
        {

            if ((u.ID == IDtarget) && (curCondition == (int)u.ConditionID))
            {
                if (u.EffectName == "work")
                {
                    Console.WriteLine("IT'S WORKING");
                }
            }
        }
    }

    }
