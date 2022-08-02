using LABA_6__Computer_.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using LABA_6__Computer_.API.Models;
using LABA_6__Computer_.Components;
using static LABA_6__Computer_.Components.DiskDrive;
using static LABA_6__Computer_.Components.RAMemory;
using System.Linq;

namespace LABA_6__Computer_
{
    public class ComputerCase : IDocumentation
    {
        private IComponent Motherboard { get; set; }
        private IComponent Processor { get; set; }
        private IComponent DiskDrive { get; set; }
        private IComponent RAMemory { get; set; }
        private IComponent GraphicsСard { get; set; }
        private IComponent WaterCooling { get; set; }
        private List<IComponent> Components => new List<IComponent>()
        {
            Motherboard, Processor, DiskDrive, RAMemory, GraphicsСard, WaterCooling
        };

        private IDocumentationMaster DocumentationMaster { get; set; }
        public ComputerCase(ComputerCaseType? Type = null)
        {
            DocumentationMaster = new DocumentationMaster();

            if (Type != null)
                switch (Type)
                {
                    case ComputerCaseType.Economy:
                        SelectMotherboard("1");
                        SelectProcessor(1, "1");
                        SelectDiskDrive(DriveType.HDD, "1");
                        SelectRAMemory(MemoryType.DDR3, 1, "1");
                        SelectGraphicsСard("1");
                        SelectWaterCooling("1");
                        break;
                    case ComputerCaseType.Middle:
                        SelectMotherboard("2");
                        SelectProcessor(2, "2");
                        SelectDiskDrive(DriveType.HDD, "2");
                        SelectRAMemory(MemoryType.DDR3, 2, "2");
                        SelectGraphicsСard("2");
                        SelectWaterCooling("2");
                        break;
                    case ComputerCaseType.Comfort:
                        SelectMotherboard("3");
                        SelectProcessor(3, "3");
                        SelectDiskDrive(DriveType.SSD, "3");
                        SelectRAMemory(MemoryType.DDR3, 3, "3");
                        SelectGraphicsСard("3");
                        SelectWaterCooling("3");
                        break;
                    case ComputerCaseType.Bliss:
                        SelectMotherboard("4");
                        SelectProcessor(4, "4");
                        SelectDiskDrive(DriveType.SSD, "4");
                        SelectRAMemory(MemoryType.DDR4, 4, "4");
                        SelectGraphicsСard("4");
                        SelectWaterCooling("4");
                        break;
                }
        }

        public void SelectMotherboard(string VendorModel)
        {
            this.Motherboard = new Motherboard(VendorModel);
        }
        public void SelectProcessor(decimal ClockRate, string VendorModel)
        {
            this.Processor = new Processor(ClockRate, VendorModel);
        }
        public void SelectDiskDrive(DriveType Type, string VendorModel)
        {
            this.DiskDrive = new DiskDrive(Type, VendorModel);
        }
        public void SelectRAMemory(MemoryType Type, int Capacity, string VendorModel)
        {
            this.RAMemory = new RAMemory(Type, Capacity, VendorModel);
        }
        public void SelectGraphicsСard(string VendorModel)
        {
            this.GraphicsСard = new GraphicsСard(VendorModel);
        }
        public void SelectWaterCooling(string VendorModel)
        {
            this.WaterCooling = new WaterCooling(VendorModel);
        }

        public string GetConfiguration()
        {
            return DocumentationMaster.GetConfiguration(this.Components.Select(x => (IDocumentation)x).ToList());
        }
    }
}
