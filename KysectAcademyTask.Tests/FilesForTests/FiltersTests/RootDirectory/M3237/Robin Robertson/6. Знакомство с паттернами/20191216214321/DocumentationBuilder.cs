using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ifmolab
{
    class DocumentationBuilder : IBuilder
    {
        private Documentation documentation = new Documentation();

        public DocumentationBuilder()
        {
            this.Reset();
        }
        public void Reset()
        {
            this.documentation = new Documentation();
        }
        public void BuildMotherBoard(String s)
        {
            documentation.Add("MotherBoard: {s}");
        }
        public void BuildProcessor(String s, double f)
        {
            documentation.Add("Processor: {s}, {f}");
        }
        public void BuildDataBase(String s, DataBaseType data)
        {
            documentation.Add("DataBase: {s}, тип = {data}");
        }
        public void BuildRAM(String s, int size, RAMType type)
        {
            documentation.Add("RAM: {s}, размер = {size} Мб, тип = {type}");
        }
        public void BuildVideoCard(String s)
        {
            documentation.Add("Видеокарта: {s}");
        }
        public void BuildCoolingWater(String s)
        {
            documentation.Add("Жидкостное охлаждение: {s}");
        }
        public Documentation GetDocumetation()
        {
            
            Documentation result = documentation;

            this.Reset();

            return result;
        }
    }
}
