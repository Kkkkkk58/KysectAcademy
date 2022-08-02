using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Heroes3;

        static class Program
        {

    static void Main()
    {

        Log log = Log.getInstance();

        string name;
        int number;
        string StrNumber;
        Writer writer = new Writer();
        List<string> list = writer.ListOfUnits();
        bool isNumeric;
        bool CorrectName;
        log.sb.AppendLine("Initialise units");
        List<UnitStack> BlueStack = new List<UnitStack>();
        Console.WriteLine("Blue, make an army");
        for (int i = 0; i < 6; i++)
        {
            CorrectName = false;
            Console.WriteLine($"now in army:{i} stacks");
            Console.WriteLine("Write a name of a minion or \"quit\" to end ");
            name = Console.ReadLine();
            if (name == "quit")
            {
                if (i == 0)
                {
                    Console.WriteLine("Please, at least 1 stack");
                    i = i - 1;
                    continue;
                }
                break;
            }

            foreach (string s in list)
            {
                if (s.ToUpper() == name.ToUpper())
                {
                    CorrectName = true;
                }
            }
            if (CorrectName == false)
            {
                Console.WriteLine("Incorrect name, plase try again");
                i = i - 1;
                continue;
            }
            Console.WriteLine("Now write amount");
            StrNumber = Console.ReadLine();
            isNumeric = int.TryParse(StrNumber, out number);
            if ((number <= 0) || (number > 999999) || (isNumeric != true))
            {
                Console.WriteLine("Incorrect number, please try again");
                i = i - 1;
                continue;
            }
            BlueStack.Add(new UnitStack(name, number));
        }

        Console.Clear();
        writer.ListOfUnits();
        List<UnitStack> RedStack = new List<UnitStack>();
        Console.WriteLine("Red turn to make army");
        for (int i = 0; i < 6; i++)
        {
            CorrectName = false;
            Console.WriteLine($"now in army:{i} stacks");
            Console.WriteLine("Write a name of a minion or \"quit\" to end ");
            name = Console.ReadLine();
            if (name == "quit")
            {
                if (i == 0)
                {
                    Console.WriteLine("Please, at least 1 stack");
                    i = i - 1;
                    continue;
                }
                break;
            }

            foreach (string s in list)
            {
                if (s.ToUpper() == name.ToUpper())
                {
                    CorrectName = true;
                }
            }
            if (CorrectName == false)
            {
                Console.WriteLine("Incorrect name, plase try again");
                i = i - 1;
                continue;
            }
            Console.WriteLine("Now write amount");
            StrNumber = Console.ReadLine();
             isNumeric = int.TryParse(StrNumber, out number);
            if (((number <= 0) || (number > 999999)) && (isNumeric != true))
            {
                Console.WriteLine("Incorrect number, please try again");
                i = i - 1;
                continue;
            }
            RedStack.Add(new UnitStack(name, number));
        }

        ArmyClass RedArmy = new ArmyClass(BlueStack);
        ArmyClass BlueArmy = new ArmyClass(RedStack);

        Battle GAME = new Battle(BlueArmy, RedArmy);


        writer.ShowLog();
        Console.WriteLine("Blue army");
        writer.Show(BlueArmy);
        Console.WriteLine("Red army");
        writer.Show(RedArmy);
        Console.ReadKey();
    }
}
