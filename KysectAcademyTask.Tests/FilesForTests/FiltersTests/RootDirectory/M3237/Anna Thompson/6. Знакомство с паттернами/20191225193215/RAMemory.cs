using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_6__Computer_.Components
{
    public class RAMemory : LABA_6__Computer_.API.Models.Component
    {
        public enum MemoryType
        {
            DDR3,
            DDR4
        }

        private MemoryType _type { get; set; }
        private int _capacity { get; set; }

        public RAMemory(MemoryType Type, int Capacity, string VendorModel) : base(VendorModel)
        {
            _type = Type;
            _capacity = Capacity;
        }

        public override string GetConfiguration() => $"RAMemory: Vendor+Model - {VendorModel}; Type - {_type.ToString()}; Capacity - {_capacity};";
    }
}
