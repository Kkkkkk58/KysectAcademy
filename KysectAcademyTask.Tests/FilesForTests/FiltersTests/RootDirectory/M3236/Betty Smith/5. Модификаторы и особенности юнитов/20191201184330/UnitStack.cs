using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class UnitStack
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
            case "CYCLOPE":
                minion = new Cyclope();
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
                throw new ArgumentException("Wrong Name. Shut down programm!"); //exception
                return;
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