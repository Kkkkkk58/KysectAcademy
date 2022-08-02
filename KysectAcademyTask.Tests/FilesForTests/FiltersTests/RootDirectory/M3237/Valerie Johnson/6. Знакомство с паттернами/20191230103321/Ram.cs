using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6._2
{
    public class Ram : ComputerPart
    {
        public RamType.Type Type { get; }
        public Ram(string model, string producer, RamType.Type type) : base(model, producer)
        {
            Type = type;
        }
    }
}
