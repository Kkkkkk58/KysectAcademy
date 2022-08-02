using System;
using System.Collections.Generic;
using System.Text;

namespace Laba
{
    public class Army
    {
        private List<UnitsStack> Arm = new List<UnitsStack>();

        public IList<UnitsStack> arm { get { return Arm.ToArray(); } }

        public Army()
        { }

        public void Add(UnitsStack x)
        {
            if (Arm.Count > 5)
            {
                Console.WriteLine("Army is full");
                Console.ReadKey();
                Console.Clear();
            }
            else
                Arm.Add(x);
        }

        public void Del(UnitsStack x)
        {
            if (Arm.Remove(x) != true)
            {
                Console.WriteLine("Unit do not exists");
                Console.ReadKey();
                Console.Clear();
            }
    }
    }
}
