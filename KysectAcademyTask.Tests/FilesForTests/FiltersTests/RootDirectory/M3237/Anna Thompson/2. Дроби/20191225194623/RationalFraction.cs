using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_2
{
    public class RationalFraction
    {
        public double up{ get; set; }
        public double down { get; set; }

        public RationalFraction(double Up, double Down)
        {
            up = Up;
            down = Down;
            if (down == 0) throw new Exception("No vallid denominator");
        }

        public static bool operator <(RationalFraction x1, RationalFraction x2)
        {
            return (x1.up * x2.down) - (x2.up * x1.down) < 0;
        }

        public static bool operator >(RationalFraction x1, RationalFraction x2)
        {
            return (x1.up * x2.down) - (x2.up * x1.down) > 0;
        }

        public static bool operator ==(RationalFraction x1, RationalFraction x2)
        {
            if ((object)x1 == null)
                if ((object)x2 == null) return true;
                else return false;

            if ((object)x2 == null)
                if ((object)x1 == null) return true;
                else return false;
            
            return (x1.up * x2.down) - (x2.up * x1.down) == 0;        
        }

        public static bool operator !=(RationalFraction x1, RationalFraction x2)
        {
            return !(x1 == x2);
        }

        public static RationalFraction operator *(RationalFraction x, double multiply)
        {
            x.up *= multiply;
            return x;
        }

        public static RationalFraction operator +(RationalFraction x1, RationalFraction x2)
        {
            return new RationalFraction(x1.up * x2.down + x2.up * x1.down, x1.down * x2.down);
        }

        /// <summary>
        /// Эквивалентоность по числителю и знаментателю
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public bool Eql (RationalFraction x)
        {
            return (up == x.up) && (down == x.down);
        }

        public void Show()
        {
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            return up + " / " + down;
        }
    }
}
