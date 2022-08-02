using LABA_6__Filter_.API.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LABA_6__Filter_.API.Interfaces;

namespace LABA_6__Filter_.Filters
{
    public class TypeFilter : Filter
    {
        private ProductType _type { get; set; }
        public TypeFilter(ProductType Type)
        {
            this._type = Type;
        }

        protected override IEnumerable<IProduct> _FilterOut(IEnumerable<IProduct> products)
        {
            return products.Where(x => x.Type == this._type).ToList();
        }
    }
}
