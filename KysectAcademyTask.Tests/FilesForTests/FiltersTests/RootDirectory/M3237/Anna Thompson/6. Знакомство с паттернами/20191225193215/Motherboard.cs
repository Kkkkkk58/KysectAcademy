using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_6__Computer_.Components
{
    public class Motherboard : LABA_6__Computer_.API.Models.Component
    {
        public Motherboard(string VendorModel) : base(VendorModel)
        {
        }

        public override string GetConfiguration() => $"Motherboard: Vendor+Model - {VendorModel};";
    }
}
