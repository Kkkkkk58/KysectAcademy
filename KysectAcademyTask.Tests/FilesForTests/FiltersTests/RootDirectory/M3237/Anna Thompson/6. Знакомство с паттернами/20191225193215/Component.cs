using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LABA_6__Computer_.API.Models
{
    public abstract class Component : LABA_6__Computer_.API.Interfaces.IComponent
    {
        public Component(string VendorModel) 
        {
            this.VendorModel = VendorModel;
        }

        public string VendorModel { get; set; }

        public abstract string GetConfiguration();
    }
}
