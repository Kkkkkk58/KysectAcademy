using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6._2
{
    public interface IBuilder
    {
            IBuilder Reset();
            IBuilder MotherBoard(MotherBoard motherboard);

            IBuilder Processor(Processor processor);

            IBuilder DataStorage(DataStorage dataStorage);

            IBuilder Ram(IEnumerable<Ram> ram);

            IBuilder GraphicsCard(GraphicsCard graphicsCard);

            IBuilder Cooling(Cooling cooling);
    }
}
