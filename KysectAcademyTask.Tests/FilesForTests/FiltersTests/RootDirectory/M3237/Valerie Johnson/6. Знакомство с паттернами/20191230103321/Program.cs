using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6._2
{
    class Program
    {
        static void Main()
        {
            ComputerBuilder b1 = new ComputerBuilder();
            MotherBoard m = new MotherBoard("aaaa", "aaaa");
            Processor p = new Processor("aaaa", "aaaa", 1000);
            DataStorage d = new DataStorage("aaaa", "aaaa", DataType.DataStorageType.HDD);
            Ram r = new Ram("aaaa", "aaaa", RamType.Type.DDR4);
            List<Ram> rs = new List<Ram>
            {
                r
            };
            b1.MotherBoard(m).Processor(p).Ram(rs).DataStorage(d);
            Computer c = b1.Get();
            Director dr = new Director();
            Documentation documentation1 = new Documentation(dr.ProComputer());
            Console.WriteLine(documentation1.MotherBoard.Model);
            Documentation documentation2 = new Documentation(dr.BaseComputer());
            Console.WriteLine(documentation2.MotherBoard.Model);
        }
    }
}
