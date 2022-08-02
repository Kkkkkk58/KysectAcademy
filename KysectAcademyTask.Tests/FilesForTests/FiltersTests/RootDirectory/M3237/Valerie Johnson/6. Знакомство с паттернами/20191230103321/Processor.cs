using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6._2
{
    public class Processor : ComputerPart
    {
        public double Frequency { get; }

        public Processor(string model, string producer, double frequency) : base(model, producer)
        {
            Frequency = frequency;
        }
    }
}
