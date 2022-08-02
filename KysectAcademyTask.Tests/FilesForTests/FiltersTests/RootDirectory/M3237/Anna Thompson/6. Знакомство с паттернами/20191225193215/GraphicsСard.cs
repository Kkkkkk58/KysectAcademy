using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_6__Computer_.Components
{
    public class GraphicsСard : LABA_6__Computer_.API.Models.Component
    {
        public GraphicsСard(string VendorModel) : base(VendorModel)
        {
        }

        public override string GetConfiguration() => $"GraphicsСard: Vendor+Model - {VendorModel};";
    }
}
