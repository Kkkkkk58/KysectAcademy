using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_v2
{
    public class Skeleton : Unit
    {
        public Skeleton() : base("Skeleton", 5, 1, 2, (1, 2), 10)
        {
            Features.Add(Feature.Cosset);
        }
    }
}