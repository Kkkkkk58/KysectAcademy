using LABA_6__Filter_.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LABA_6__Filter_.API.Models
{
    public abstract class Filter : IFilter
    {
        public Filter() 
        { 
        }

        private IFilter _nextFilter { get; set; }

        public IFilter SetNext(IFilter filter)
        {
            this._nextFilter = filter;
            return filter;
        }

        public IEnumerable<IProduct> FilterOut(IEnumerable<IProduct> products)
        { 
            if (this._nextFilter != null)
            {
                return this._nextFilter.FilterOut(_FilterOut(products));
            }
            else
            {
                return _FilterOut(products);
            }
        }

        protected abstract IEnumerable<IProduct> _FilterOut(IEnumerable<IProduct> products);
    }
}
