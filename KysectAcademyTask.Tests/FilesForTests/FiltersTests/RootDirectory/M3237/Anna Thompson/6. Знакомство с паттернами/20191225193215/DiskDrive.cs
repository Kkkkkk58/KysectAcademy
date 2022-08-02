using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LABA_6__Computer_.Components
{
    public class DiskDrive : LABA_6__Computer_.API.Models.Component
    {
        public enum DriveType
        {
            HDD,
            SSD
        }

        private DriveType _type { get; set; }

        public DiskDrive(DriveType Type, string VendorModel) : base(VendorModel)
        {
            _type = Type;
        }

        public override string GetConfiguration() => $"DiskDrive: Vendor+Model - {VendorModel}; Type - {_type.ToString()};";
    }
}
