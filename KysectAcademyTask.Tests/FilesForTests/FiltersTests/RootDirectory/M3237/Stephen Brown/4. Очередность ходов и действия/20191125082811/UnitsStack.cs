using System;
using System.Collections.Generic;
using System.Text;

namespace Laba
{
    public class UnitsStack
    {
        private Unit Heroes;
        private int Count;

        public Unit heroes { get { return Heroes; } }
        public int count { get { return Count; } }

        public UnitsStack(Unit Heroes, int Count)
        {
            this.Heroes = Heroes;
            this.Count = Count;
        }
    }
}
