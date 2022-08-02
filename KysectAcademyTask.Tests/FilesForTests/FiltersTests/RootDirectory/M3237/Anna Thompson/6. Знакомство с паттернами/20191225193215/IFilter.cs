using LABA_6__Filter_.API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_6__Filter_.API.Interfaces
{
    public interface IFilter
    {
        IFilter SetNext(IFilter filter);
        IEnumerable<IProduct> FilterOut(IEnumerable<IProduct> products);
    }
}
