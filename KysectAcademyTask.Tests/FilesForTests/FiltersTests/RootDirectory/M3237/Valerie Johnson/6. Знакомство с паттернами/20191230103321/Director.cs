using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6._2
{
    public class Director
    {
        private ComputerBuilder computerBuilder = new ComputerBuilder();
        public Computer BaseComputer()
        {

            computerBuilder.Reset();
            computerBuilder.MotherBoard(new MotherBoard("Base MB", "Producer1"));
            computerBuilder.Processor(new Processor("Base Proc", "Producer1", 1000));
            computerBuilder.DataStorage(new DataStorage("Base DS", "Producer1", DataType.DataStorageType.HDD));
            computerBuilder.Ram(new[] { new Ram("Base RAM", "Producer1", RamType.Type.DDR3) });
            return computerBuilder.Get();
        }

        public Computer StandartComputer()
        {
            computerBuilder.Reset();
            computerBuilder.MotherBoard(new MotherBoard("Stndart MB", "Producer2"));
            computerBuilder.Processor(new Processor("Stndart Proc", "Producer2", 2000));
            computerBuilder.DataStorage(new DataStorage("Stndart DS", "Producer2", DataType.DataStorageType.HDD));
            computerBuilder.Ram(new[] { new Ram("Stndart RAM", "Producer2", RamType.Type.DDR4) });
            computerBuilder.GraphicsCard(new GraphicsCard("Stndart GC", "Producer2"));
            return computerBuilder.Get();
        }
        public Computer ProComputer()
        {
            computerBuilder.Reset();
            computerBuilder.MotherBoard(new MotherBoard("Pro MB", "Producer3"));
            computerBuilder.Processor(new Processor("Pro Proc", "Producer3", 4000));
            computerBuilder.DataStorage(new DataStorage("Pro DS", "Producer3", DataType.DataStorageType.SSD));
            computerBuilder.Ram(new[]
            {
                new Ram("Pro RAM", "Producer3", RamType.Type.DDR4),
                new Ram("Pro RAM", "Producer3", RamType.Type.DDR4)
            });
            computerBuilder.GraphicsCard(new GraphicsCard("Pro GC", "Producer3"));
            computerBuilder.Cooling(new Cooling("Pro Cooling" , "Producer3"));
            return computerBuilder.Get();
        }
    }
}
