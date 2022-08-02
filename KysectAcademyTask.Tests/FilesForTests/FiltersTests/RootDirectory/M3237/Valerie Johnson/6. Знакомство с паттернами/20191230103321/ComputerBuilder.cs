using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6._2
{
    public class ComputerBuilder : IBuilder
    {
        private Computer computer = new Computer();

        public IBuilder Reset()
        {
            computer.MotherBoard = null;
            computer.Processor = null;
            computer.DataStorage = null;
            computer.Ram = null;
            computer.GraphicsCard = null;
            computer.Cooling = null;
            return this;
              
        }
        public IBuilder MotherBoard(MotherBoard motherboard)
        {
            computer.MotherBoard = motherboard;
            return this;
        }

        public IBuilder Processor(Processor processor)
        {
            computer.Processor = processor;
            return this;
        }

        public IBuilder DataStorage(DataStorage dataStorage)
        {
            computer.DataStorage = dataStorage;
            return this;
        }

        public IBuilder Ram(IEnumerable<Ram> ram)
        {
            computer.Ram = new List<Ram>(ram);
            return this;

        }

        public IBuilder GraphicsCard(GraphicsCard graphicsCard)
        {
            computer.GraphicsCard = graphicsCard;
            return this;
        }

        public IBuilder Cooling(Cooling cooling)
        {
            computer.Cooling = cooling;
            return this;
        }

        public Computer Get()
        {
            if (computer.MotherBoard == null || computer.Processor == null || computer.DataStorage == null || computer.Ram == null || computer.Ram.Count == 0)
            {
                throw new InvalidOperationException("Equipment isn't full");
            }
            return computer;
        }
    }
}

