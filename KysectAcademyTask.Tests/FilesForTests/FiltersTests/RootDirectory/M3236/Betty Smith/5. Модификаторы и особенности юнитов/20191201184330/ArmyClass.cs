using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class ArmyClass
    {
    public readonly List<UnitStack> Description = new List<UnitStack>();

        public ArmyClass()
        {

        }
    public ArmyClass(List<UnitStack> SaveArmy)
    {
        if (SaveArmy.Count > 6)
        {
            throw new ArgumentException("SIZE TOO BIG.");
            //exception
        }
        Description = new List<UnitStack>(SaveArmy);
    }

    public bool Add(string AddType, int AddAmount)
    {
        if (Description.Count() == 6)
        { 
            return false;
        }
        Description.Insert(Description.Count(), new UnitStack(AddType, AddAmount));
        foreach (UnitStack u in Description)
        {
            if (u.Amount == 0)
            {
                Description.RemoveAt(Description.Count() - 1);
                return false;
            }
        }
        return true;
    }

        public int Size() //more comfortable to check
        {
            return Description.Count();
        }


    }
