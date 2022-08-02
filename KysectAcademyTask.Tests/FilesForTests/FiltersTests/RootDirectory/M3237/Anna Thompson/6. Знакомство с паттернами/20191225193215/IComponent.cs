using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_6__Computer_.API.Interfaces
{
    public interface IComponent : IDocumentation
    {
        string VendorModel { get; set; }
    }
}
