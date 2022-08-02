using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6._2
{
    public class Computer
    {
        public MotherBoard MotherBoard { get; set; }
        public Processor Processor { get; set; }
        public DataStorage DataStorage { get; set; }
        public List<Ram> Ram { get; set; }
        public GraphicsCard GraphicsCard { get; set; }
        public Cooling Cooling { get; set; }
        public Computer()
        { 
        }
        public Computer(Documentation documentation)
        {
            MotherBoard = documentation.MotherBoard;
            Processor = documentation.Processor;
            DataStorage = documentation.DataStorage;
            Ram = documentation.Ram;
            GraphicsCard = documentation.GraphicsCard;
            Cooling = documentation.Cooling;
        }
    }
}
