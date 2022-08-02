using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
public class UnitStack
{
    public int Amount { get; protected set; }
    public Unit minion { get; protected set; }

    protected UnitStack() { }



    public UnitStack(string mobStr, int StackAmount)
    {
        mobStr = mobStr.ToUpper();
        switch (mobStr)
        {
            case "ARBALET":
                minion = new Arbalet();
                break;
            case "ANGEL":
                minion = new Angel();
                break;
            case "LICH":
                minion = new Lich();
                break;
            case "BONEDRAGON":
                minion = new BoneDragon();
                break;
            case "DEVIL":
                minion = new Devil();
                break;
            case "FURY":
                minion = new Fury();
                break;
            case "GRYPHONE":
                minion = new Gryphone();
                break;
            case "HYDRA":
                minion = new Hydra();
                break;
            case "SKELETON":
                minion = new Skeleton();
                break;
            case "SHAMAN":
                minion = new Shaman();
                break;
            default:
                List<Assembly> allAssemblies = new List<Assembly>();
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                foreach (string dll in Directory.GetFiles(path, "*.dll"))
                    allAssemblies.Add(Assembly.LoadFile(dll));
                foreach (Assembly ass in allAssemblies)
                {
                    Type[] types = ass.GetTypes();
                    foreach (Type tc in types)

                    {
                        if (tc.Name.ToUpper() == mobStr)
                        {
                            minion = (Unit)ass.CreateInstance($"{tc.Name}");
                        }
                    }
                }

                if (minion == null)
                {
                    throw new Exception("Unit not found");
                }
                break;
        }
        if (StackAmount > 999999 || StackAmount < 0)
        {
            Amount = 0;
        }
        else
        {
            Amount = StackAmount;
        }
    }

}