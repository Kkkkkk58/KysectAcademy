using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6._2
{
    public class DataStorage : ComputerPart
    {
        public DataType.DataStorageType Type { get; }

        public DataStorage(string model, string producer, DataType.DataStorageType type) : base(model, producer)
        {
            Type = type;
        }
    }
}
