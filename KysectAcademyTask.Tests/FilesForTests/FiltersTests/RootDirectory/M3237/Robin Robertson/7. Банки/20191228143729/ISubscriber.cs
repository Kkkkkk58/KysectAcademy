using System;
using System.Collections.Generic;
using System.Text;

namespace OOPLab7.Observers
{
    interface ISubscriber
    {
        public void UpdateDay();
        public void UpdateMonth();
    }
}
