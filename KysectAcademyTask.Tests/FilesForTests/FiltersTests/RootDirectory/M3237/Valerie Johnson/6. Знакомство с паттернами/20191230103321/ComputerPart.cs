using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6._2
{
    public class ComputerPart
    {
        public string Model { get; }
        public string Producer { get; }
        public ComputerPart(string model, string producer)
        {
            Model = model;
            Producer = producer;
        }
    }
}
