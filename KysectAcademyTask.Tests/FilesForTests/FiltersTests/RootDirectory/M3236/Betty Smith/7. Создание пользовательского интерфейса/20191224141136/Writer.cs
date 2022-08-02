using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

public class Writer
{
    public int Compare(BattleUnitStack x, BattleUnitStack y)
    {
        if (x.curInitiative == 0 || y.curInitiative == 0)
        {
            return 0;
        }

        // CompareTo() method 
        return y.curInitiative.CompareTo(x.curInitiative);

    }
    public void MainInfo(List<BattleUnitStack> queue,BattleArmy BLUE,BattleArmy RED)
    {
        Console.Clear();
        int BLUEcount = BLUE.Description.Count;
        int REDcount = RED.Description.Count;
        int max = Math.Max(BLUEcount,REDcount);
        string str1 ="";
        string str2 ="";
        string write = "";
        double Alive; 
        for (int i=0; i < max; i++)
        {
            str1 = "";
            str2 = "";
            if (i < BLUEcount)
            {
                Alive = (double)BLUE.Description[i].curHitPoints / BLUE.Description[i].minion.HitPoints;
                Alive = Math.Ceiling(Alive);
                str1 = $"{BLUE.Description[i].ID}:{BLUE.Description[i].minion.Type}:{Alive}";
            }
            if (i < REDcount)
            {
                Alive = (double)RED.Description[i].curHitPoints / RED.Description[i].minion.HitPoints;
                Alive = Math.Ceiling(Alive);
                str2 = $"{RED.Description[i].ID}:{RED.Description[i].minion.Type}:{Alive}";
            }
            write = String.Format("{0} {1}", str1.PadRight(20), str2.PadRight(20));
            Console.WriteLine(write);
        }
        //Console.Clear();

        Console.WriteLine("v - you're here");
        ShowQueue(queue);
        Console.WriteLine("//////////LOG//////////");
        Log log = Log.getInstance();
        string[] lines = log.sb.ToString().Split('\n');
       Console.WriteLine(lines[lines.Length - 4]);
        Console.WriteLine(lines[lines.Length - 3]);
        Console.WriteLine(lines[lines.Length - 2]);
        Console.WriteLine("///////////////////////");


        Console.WriteLine();


    }

    public void ShowLog()
    {
        Log log = Log.getInstance();
        Console.Clear();
        Console.WriteLine("///////////////LOG///////////////");
        Console.WriteLine(log.sb);
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    public void YourTurn(BattleUnitStack stack)
    {
        Console.WriteLine($"Your turn: {stack.Side}, choose your action\n" +
            $"1.Attack\n" +
            $"2.Cast\n" +
            $"3.Wait\n" +
            $"4.Defend\n" +
            $"5.Give up\n" +
            $"6.Show stats\n" +
            $"7.Show full log\n");
    }

    public void Target(string side)
    {
        if (side == "enemy") {
            Console.WriteLine("Choose enemy target");
                }
        if (side == "ally")
        {
            Console.WriteLine("Choose ally target");
        }
        if (side == "spell")
        {
            Console.WriteLine("Choose spell target. If there is no valid target, write \"quit\" and lose your turn");
        }
        if (side == "any")
        {
            Console.WriteLine("Choose any target");
        }
    }

    // battlestats
    public void Show(BattleUnitStack target)
    {
        string stats = "";
        double Alive = (double)target.curHitPoints / target.minion.HitPoints;
        Alive = Math.Ceiling(Alive);

        stats = $"ID: {target.ID}\n" +
    $"Name: {target.minion.Type}\n" +
    $"Amount: {Alive}\\{target.BasicAmount} HP: {target.curHitPoints - ((Alive - 1) * target.minion.HitPoints)}\\{target.minion.HitPoints}\n" +
    $"Attack: {target.minion.Attack} ({target.curAttack}) \n" +
    $"Damage: {target.minion.MinDamage} - {target.minion.MaxDamage}\n" +
    $"Defence: {target.minion.Defence} ({target.curDefence})\n" +
    $"Initiative: {target.minion.Initiative} ({target.curInitiative})\n" +
    $"Description:\n" +
    $"{target.minion.Description}\n";

        Console.WriteLine(stats);
    }

    //queue
    public void ShowQueue(List<BattleUnitStack> queue)
    {
        int first = 1;
        List<BattleUnitStack> SecondQueue = new List<BattleUnitStack>(queue);
        SecondQueue.Sort(Compare);
        foreach (BattleUnitStack u in queue)
        {
            if (u.queueInitiative != -900)
            {
                if (first == 1)
                {
                    Console.Write($"{u.ID}:{u.minion.Type} ");
                    first = 0;
                }
                else
                {
                    Console.Write($"-> {u.ID}:{u.minion.Type} ");
                }
            }
        }
        Console.Write("|");
        first = 1;
        foreach (BattleUnitStack u in SecondQueue)
        {
            if (first == 1)
            {
                Console.Write($"{u.ID}:{u.minion.Type} ");
                first = 0;
            }
            else
            {
                Console.Write($"-> {u.ID}:{u.minion.Type} ");
            }
        }
        Console.WriteLine();
    }

    //unit
    public void Show(Unit u)
    {
        string stats;
        stats = $"Name: {u.Type}\n" +
            $"HP: {u.HitPoints}\n" +
            $"Attack: {u.Attack}\n" +
            $"Damage: {u.MinDamage} - {u.MaxDamage}\n" +
            $"Defence: {u.Defence}\n" +
            $"Initiative: {u.Initiative}\n";
        Console.WriteLine(stats);
    }
    //Army
    public void Show(ArmyClass army)
    {
        string ArmyShow = "";
        foreach (UnitStack u in army.Description)
        {
            //unit
            string stats;
            stats = $"Name: {u.minion.Type}\n" +
                $"HP: {u.minion.HitPoints}\n" +
                $"Attack: {u.minion.Attack}\n" +
                $"Damage: {u.minion.MinDamage} - {u.minion.MaxDamage}\n" +
                $"Defence: {u.minion.Defence}\n" +
                $"Initiative: {u.minion.Initiative}\n";
            //
            ArmyShow = ArmyShow + $"///{u.minion.Type}:{u.Amount}///\n" +
                $"{stats}\n";
        }
        if (ArmyShow == "")
        {
            Console.WriteLine("Empty army");
        }
        else
        {
            Console.WriteLine(ArmyShow);
        }
    }

    //many battlestats
    public void Show(BattleArmy barmy)
    {
            string ArmyShow = "";
            foreach (BattleUnitStack u in barmy.Description)
            {
            //battlestat
            string stats = "";
            double Alive = (double)u.curHitPoints / u.minion.HitPoints;
            Alive = Math.Ceiling(Alive);

            stats = $"ID: {u.ID}\n" +
        $"Name: {u.minion.Type}\n" +
        $"Amount: {Alive}\\{u.BasicAmount} HP: {u.curHitPoints - ((Alive - 1) * u.minion.HitPoints)}\\{u.minion.HitPoints}\n" +
        $"Attack: {u.minion.Attack} ({u.curAttack}) \n" +
        $"Damage: {u.minion.MinDamage} - {u.minion.MaxDamage}\n" +
        $"Defence: {u.minion.Defence} ({u.curDefence})\n" +
        $"Initiative: {u.minion.Initiative} ({u.curInitiative})\n" +
        $"Description:\n" +
        $"{u.minion.Description}\n";
            //

            ArmyShow = ArmyShow + $"///{u.minion.Type}:{u.BasicAmount}///\n" +
                    $"{stats}\n";
            }
            if (ArmyShow == "")
            {
                Console.WriteLine("Empty army");
            }
            else
            {
                Console.WriteLine(ArmyShow);
            }
    }

    public List<string> ListOfUnits()
    {
        Console.WriteLine("Choose from this units:");
        Console.WriteLine("Base units:");
        List<string> list = new List<string>{"Angel",
            "Arbalet" ,
            "BoneDragon" ,
            "Devil" ,
            "Fury" ,
            "Gryphone" ,
            "Hydra" ,
            "Lich" ,
            "Shaman" ,
            "Skeleton" , };
        foreach (string s in list)
        {
            Console.WriteLine(s);
        }
        Console.WriteLine("Mod Units:");
        List<Assembly> allAssemblies = new List<Assembly>();
        string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        foreach (string dll in Directory.GetFiles(path, "*.dll"))
            allAssemblies.Add(Assembly.LoadFile(dll));
        foreach (Assembly ass in allAssemblies)
        {
            Type[] types = ass.GetTypes();
            foreach (Type tc in types)

            {
                list.Add(tc.Name);
                Console.WriteLine(tc.Name);
            }
        }
        return list;
    }
}