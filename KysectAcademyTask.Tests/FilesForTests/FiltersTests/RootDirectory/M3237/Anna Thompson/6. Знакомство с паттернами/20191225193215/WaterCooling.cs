using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_6__Computer_.Components
{
    public class WaterCooling : LABA_6__Computer_.API.Models.Component
    {
        public WaterCooling(string VendorModel) : base(VendorModel)
        {
        }

        public override string GetConfiguration() => $"WaterCooling: Vendor+Model - {VendorModel};";
    }
}
