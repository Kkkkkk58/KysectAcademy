using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_6__Computer_.Components
{
    public class Processor : LABA_6__Computer_.API.Models.Component
    {
        private decimal _clockRate { get; set; }

        public Processor(decimal ClockRate, string VendorModel) : base(VendorModel)
        {
            _clockRate = ClockRate;
        }

        public override string GetConfiguration() => $"Processor: Vendor+Model - {VendorModel}; Clock rate - {_clockRate};";
    }
}
