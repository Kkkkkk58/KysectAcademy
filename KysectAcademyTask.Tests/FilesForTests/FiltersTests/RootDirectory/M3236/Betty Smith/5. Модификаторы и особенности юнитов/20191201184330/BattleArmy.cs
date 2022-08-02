using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class BattleArmy
{
    public List<BattleUnitStack> Description = new List<BattleUnitStack>();

    public BattleArmy(ArmyClass CrusadeArmy)
    {
        foreach (UnitStack u in CrusadeArmy.Description)
        {
            Add(new BattleUnitStack(u));
        }
    }

    public int Add(BattleUnitStack NewStack)
    {
        if (Description.Count() == 9)
        {
            return 0;
        }
        Description.Insert(Description.Count(), NewStack);

        return 1;
    }

    public bool IsDead(BattleUnitStack Health)
    {
        if (Health.curHitPoints <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void RemoveDead() // should be summoned after each attack
    {
        Description.RemoveAll(IsDead);
    }

    public bool IsDefeated()
    {
        if (Description.Count() == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Surrender()
    {
        Description.Clear();
    }
}