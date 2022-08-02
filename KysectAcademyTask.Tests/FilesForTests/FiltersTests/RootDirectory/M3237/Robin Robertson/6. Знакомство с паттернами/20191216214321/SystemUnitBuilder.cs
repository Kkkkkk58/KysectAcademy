using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ifmolab
{
    public class SystemUnitBuilder : IBuilder
    {
        private SystemUnit systemUnit = new SystemUnit();

        public SystemUnitBuilder()
        {
            this.Reset();
        }
        public void Reset()
        {
            this.systemUnit = new SystemUnit();
        }
        public void BuildMotherBoard(String s)
        {
            Detail detail = new Detail() { ModelAndPower = s };
            systemUnit.motherBoard = detail;
        }
        public void BuildProcessor(String s, double f)
        {
            Processor processor = new Processor() { ModelAndPower = s, Frequency = f };
            systemUnit.processor = processor;
        }
        public void BuildDataBase(String s, DataBaseType data)
        {
            DetailDataBase detail = new DetailDataBase() { ModelAndPower = s, Type = data };
            systemUnit.dataBase = detail;
        }
        public void BuildRAM(String s, int size, RAMType type)
        {
            RAM detail = new RAM() { ModelAndPower = s, Size = size, Type = type };
            systemUnit.ram.Add(detail);
        }
        public void BuildVideoCard(String s)
        {
            Detail detail = new Detail() { ModelAndPower = s };
            systemUnit.videoCard = detail;
        }
        public void BuildCoolingWater(String s)
        {
            Detail detail = new Detail() { ModelAndPower = s };
            systemUnit.CoolingWater = detail;
        }
        public SystemUnit GetSystemUnit(out Documentation documentation)
        {
            if (systemUnit.motherBoard == null || systemUnit.processor == null || systemUnit.dataBase == null|| systemUnit.ram == null)
            {
                throw new Exception("не все детали добавлены");
            }
            var builder = new DocumentationBuilder();
            builder.BuildMotherBoard(systemUnit.motherBoard.ModelAndPower);
            builder.BuildProcessor(systemUnit.processor.ModelAndPower, systemUnit.processor.Frequency);
            builder.BuildDataBase(systemUnit.dataBase.ModelAndPower, systemUnit.dataBase.Type);
            foreach (RAM i in systemUnit.ram)
            {
                builder.BuildRAM(i.ModelAndPower,i.Size,i.Type);
            }
            if (systemUnit.videoCard != null)
            {
                builder.BuildVideoCard(systemUnit.videoCard.ModelAndPower);
            }
            if (systemUnit.CoolingWater != null)
            {
                builder.BuildCoolingWater(systemUnit.CoolingWater.ModelAndPower);
            }
            documentation = builder.GetDocumetation();

            SystemUnit result = systemUnit;

            this.Reset();

            return result;
        }
    }
}
