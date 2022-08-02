using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ifmolab
{
    public interface IBuilder
    {
        void BuildMotherBoard(String s);
        void BuildProcessor(String s, double f);
        void BuildDataBase(String s, DataBaseType data);
        void BuildRAM(String s, int size, RAMType type);
        void BuildVideoCard(String s);
        void BuildCoolingWater(String s);
    }

    public enum DataBaseType
    {
        HARDDISK,
        SSD
    }
    public enum RAMType
    {
        DDR3 = 3,
        DDR4
    }
    public class Detail
    {
        public String ModelAndPower { get; set; }
    }
    public class Processor : Detail
    {
        public double Frequency { get; set; }
    }
    public class DetailDataBase : Detail
    {
        public DataBaseType Type { get; set; }
    }
    public class RAM : Detail
    {
        public RAMType Type { get; set; }
        public int Size { get; set; }
    }
    public class SystemUnit
    {
        public Detail motherBoard { get; set; }
        public Processor processor { get; set; }
        public DetailDataBase dataBase { get; set; }
        public List<RAM> ram { get; set; }
        public Detail videoCard { get; set; }
        public Detail CoolingWater { get; set; }


    }
    public class Documentation
    {
        String documentation;
        public void Add(String part)
        {
            documentation += part+'\n';
        }
        public String getDocumentation()
        {
            return documentation;
        }

    }

    public class Director
    {
        private IBuilder builder;
        public IBuilder Builder
        {
            set { builder = value; }
        }
        public void Econom()
        {
            this.builder.BuildMotherBoard("ASUS PRIME Z270 - P");
            this.builder.BuildProcessor("Intel Core i3-6006U", 2.0);
            this.builder.BuildDataBase("Name", (DataBaseType)0);
            this.builder.BuildRAM("L Memory", 4, (RAMType)3);
        }
        public void Middle()
        {
            this.builder.BuildMotherBoard("ASUS PRIME Z270 - P");
            this.builder.BuildProcessor("Intel Core i3-7880U", 2.3);
            this.builder.BuildDataBase("Name 2.0", (DataBaseType)1);
            this.builder.BuildRAM("L Memory", 4, (RAMType)3);
            this.builder.BuildRAM("L Memory", 4, (RAMType)3);
        }
        public void High()
        {
            this.builder.BuildMotherBoard("ASUS PREMIUM X8808 - G");
            this.builder.BuildProcessor("Intel Core i5-2320W", 3.1);
            this.builder.BuildDataBase("Name 2.0", (DataBaseType)1);
            this.builder.BuildRAM("L Memory", 8, (RAMType)4);
            this.builder.BuildVideoCard("NVIDIA GeForce 940MX with 2GB");
        }
        public void Top()
        {
            this.builder.BuildMotherBoard("ASUS PREMIUM X8808 - G");
            this.builder.BuildProcessor("Intel Core i9-1001A", 3.5);
            this.builder.BuildDataBase("Name 2.5", (DataBaseType)1);
            this.builder.BuildRAM("L Memory", 8, (RAMType)4);
            this.builder.BuildRAM("L Memory", 4, (RAMType)4);
            this.builder.BuildVideoCard("NVIDIA GeForce 940MX with 4GB");
            this.builder.BuildCoolingWater("COOLINGWATER3000");
        }
    }

    public class Lab6_2
    {
        public Lab6_2()
        {
            var director = new Director();
            var builder = new SystemUnitBuilder();
            director.Builder = builder;
            director.Econom();
            Documentation documentation;
            SystemUnit sU = builder.GetSystemUnit(out documentation);
            Console.Write(documentation.getDocumentation());
            builder.BuildMotherBoard("ASUS PREMIUM X8808 - G");
            builder.BuildProcessor("Intel Core i9-1001A", 3.0);
            builder.BuildRAM("Memory", 4, (RAMType)4);
            sU = builder.GetSystemUnit(out documentation);
        }

    }
}
