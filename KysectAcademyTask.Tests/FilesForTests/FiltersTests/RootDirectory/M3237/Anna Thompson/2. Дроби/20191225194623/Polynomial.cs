using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_2
{
    public class Polynomial
    {
        private List<RationalFraction> _coefficients { get; set; }
        public int Count => _coefficients.Count;
        public List<RationalFraction> Coefficients => _coefficients;

        public Polynomial()
        {
            _coefficients = new List<RationalFraction>();
        }

        public Polynomial(SetFractions sf)
        {
            _coefficients = sf.GetFractions();
        }

        public RationalFraction Result(double X)
        {
            RationalFraction sum = new RationalFraction(0, 1);

            for (var i = 0; i < _coefficients.Count; i++)
            {
                sum = sum + _coefficients[i] * Math.Pow(X, _coefficients.Count - i - 1);
            }

            return sum;
        }

        public static Polynomial operator +(Polynomial x1, Polynomial x2)
        {
            var size = Math.Max(x1.Count, x2.Count);
            SetFractions set = new SetFractions();
            for (var i = 0; i < size; i++)
            {
                RationalFraction f = new RationalFraction(0, 1);
                if (x1.Count > i)
                {
                    f += x1.Coefficients[i];
                }
                if (x2.Count > i)
                {
                    f += x2.Coefficients[i];
                }
                set.Add(f);
            }
            return new Polynomial(set);
        }
    }
}
