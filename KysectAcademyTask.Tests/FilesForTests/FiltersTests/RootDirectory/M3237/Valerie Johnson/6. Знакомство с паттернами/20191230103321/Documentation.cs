using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6._2
{
    public class Documentation
    {
        public MotherBoard MotherBoard { get; }
        public Processor Processor { get; }
        public DataStorage DataStorage { get; }
        public List<Ram> Ram { get; }
        public GraphicsCard GraphicsCard { get; }
        public Cooling Cooling { get; }

        public Documentation()
        {
        }
        public Documentation(Computer computer)
        {
            MotherBoard = computer.MotherBoard;
            Processor = computer.Processor;
            DataStorage = computer.DataStorage;
            Ram = computer.Ram;
            GraphicsCard = computer.GraphicsCard;
            Cooling = computer.Cooling;
        }   
    }
}
