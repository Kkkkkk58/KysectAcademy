using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6._2
{
    class DocumentationBuilder : IBuilder
    {
        private Computer documentation = new Computer();

        public IBuilder Reset()
        {
            documentation.MotherBoard = null;
            documentation.Processor = null;
            documentation.DataStorage = null;
            documentation.Ram = null;
            documentation.GraphicsCard = null;
            documentation.Cooling = null;
            return this;

        }
        public IBuilder MotherBoard(MotherBoard motherboard)
        {
            documentation.MotherBoard = motherboard;
            return this;
        }

        public IBuilder Processor(Processor processor)
        {
            documentation.Processor = processor;
            return this;
        }

        public IBuilder DataStorage(DataStorage dataStorage)
        {
            documentation.DataStorage = dataStorage;
            return this;
        }

        public IBuilder Ram(IEnumerable<Ram> ram)
        {
            documentation.Ram = new List<Ram>(ram);
            return this;

        }

        public IBuilder GraphicsCard(GraphicsCard graphicsCard)
        {
            documentation.GraphicsCard = graphicsCard;
            return this;
        }

        public IBuilder Cooling(Cooling cooling)
        {
            documentation.Cooling = cooling;
            return this;
        }

        public Computer Get()
        {
            if (documentation.MotherBoard == null || documentation.Processor == null || documentation.DataStorage == null || documentation.Ram == null || documentation.Ram.Count == 0)
            {
                throw new InvalidOperationException("Equipment isn't full");
            }
            return documentation;
        }
    }
}

