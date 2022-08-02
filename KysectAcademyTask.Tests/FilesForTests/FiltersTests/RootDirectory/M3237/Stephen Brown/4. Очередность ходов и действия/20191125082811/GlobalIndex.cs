using System;
using System.Collections.Generic;
using System.Text;

namespace Laba
{
    public class GInd
    {
        static private int Ind = 0;
        static public int ind { get { return Ind; } set { Ind = value; } }
    }
}
