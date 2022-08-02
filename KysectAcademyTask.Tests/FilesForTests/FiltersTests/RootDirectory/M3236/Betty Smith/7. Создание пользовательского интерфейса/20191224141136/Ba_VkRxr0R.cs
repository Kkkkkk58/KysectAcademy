using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BattleUnitStack
{
    public int curHitPoints { get; private set; }
        public int curAttack { get; private set; }
        public int curDefence { get; private set; }
    public double curInitiative { get; private set; }

    public int BasicAmount { get; protected set; }
    public Unit minion { get; protected set; }
    
    //battle stuff
    public int ID { get; protected set; }
    public double queueInitiative { get; private set; }
    public string Side { get; protected set; }
    public int counter { get; protected set; }
    public int isDefend { get; protected set; }
    public int isWaited { get; protected set; }
    public int Spell { get; protected set; }
    public void setSpell(int set)
    {
        Spell = set;
    }
    public void setIsWaited(int set)
    {
        isWaited = set;
    }
    public void setCounter(int set)
    {
        counter = set;
    }
    public void setIsDefend(int set)
    {
        isDefend = set;
    }
    public void setID(int set)
    {
        ID = set;
        set = set + 1;
    }
    public void setSide(string side)
    {
        Side = side;
    }

    public void changeQueueInitiative(double set)
    {
        queueInitiative = set;
    }
    //battle stuff

    public BattleUnitStack(UnitStack CrusadeStack)
    {
        BasicAmount = CrusadeStack.Amount; // max possible amount of minions
        this.minion = CrusadeStack.minion; 
        curHitPoints = CrusadeStack.minion.HitPoints * CrusadeStack.Amount;
        curAttack = CrusadeStack.minion.Attack;
        curDefence = CrusadeStack.minion.Defence;
        curInitiative = CrusadeStack.minion.Initiative;
        queueInitiative = curInitiative;
    }
    public bool IsDead()
    {
        if (curHitPoints <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //apply changes to stats (heal/attack, buff/debuff)
    public void changeHealth(int Health) //+healed -damaged
    {
        double wasAlive = (double)curHitPoints / minion.HitPoints;
        wasAlive = Math.Ceiling(wasAlive);

        curHitPoints = curHitPoints + Health;
        if (curHitPoints <= 0)
        {
            curHitPoints = 0;
        }
        if (curHitPoints > minion.HitPoints * BasicAmount)
        {
            curHitPoints = minion.HitPoints * BasicAmount;
        }

        double nowAlive = (double)curHitPoints / minion.HitPoints;
        nowAlive = Math.Ceiling(nowAlive);

        if (Health > 0)
        {
            Log log = Log.getInstance();
            log.sb.AppendLine($"{ID}:{minion.Type} get healed on {Health} HP; You resurected {nowAlive - wasAlive}; You now have {nowAlive}");

        }
        else
        {
            Log log = Log.getInstance();
            log.sb.AppendLine($"{ID}:{minion.Type} take {-Health} damage; You lost {wasAlive - nowAlive} minions; You now have {nowAlive}");
            if (nowAlive == 0)
            {
                log.sb.AppendLine($"{ID}:{minion.Type} died");
            }
        }
    }
    public void changeAttack(int attack)
    {

        curAttack = curAttack + attack;
        if (curAttack < 0)
        {
            curAttack = 0;
        }
        Log log = Log.getInstance();
        log.sb.AppendLine($"{ID}:{minion.Type} get {attack} attack; current attack = {curAttack}");
    }
    public void changeDefence(int defence)
    {

        curDefence = curDefence + defence;
        if (curDefence < 0)
        {
            curDefence = 0;
        }
        Log log = Log.getInstance();
        log.sb.AppendLine($"{ID}:{minion.Type} get {defence} defence; current defence = {curDefence}");
    }
    public void changeInitiative(double initiative)
    {
        curInitiative = curInitiative + initiative;
        if (curInitiative < 0)
        {
            curInitiative = 0;
        }
        Log log = Log.getInstance();
        log.sb.AppendLine($"{ID}:{minion.Type} get {initiative} initiative; current initiative = {curInitiative}");
    }
}