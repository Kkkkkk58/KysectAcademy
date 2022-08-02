using System;
using System.Collections.Generic;
using System.Text;

namespace Laba
{
    public class Activ
    {
        private bool Ctive = true;
        public bool ctive { get { return Ctive; } set { Ctive = value; } }
    }

    public class Time
    {
        private int Ime = 0;
        public Time(int x)
        {
            Ime = x;
        }
        public int ime { get { return Ime; } set { Ime = value; } }
    }

    public class Value
    {
        private double Alue = 0;
        public Value(double x)
        {
            Alue = x;
        }
        public double alue { get { return Alue; } set { Alue = value; } }
    }
    public class Cntr
    {
        private bool Onter = false;
        public Cntr(bool x)
        {
            Onter = x;
        }
        public bool onter { get { return Onter; } set { Onter = value; } }
    }
}
