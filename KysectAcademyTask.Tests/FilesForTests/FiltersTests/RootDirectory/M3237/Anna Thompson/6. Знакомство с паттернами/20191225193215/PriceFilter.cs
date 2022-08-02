using LABA_6__Filter_.API.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LABA_6__Filter_.API.Interfaces;

namespace LABA_6__Filter_.Filters
{
    public class PriceFilter : Filter
    {
        private decimal? _minPrice { get; set; }
        private decimal? _maxPrice { get; set; }

        public PriceFilter(decimal? MinPrice = null, decimal? MaxPrice = null)
        {
            _minPrice = MinPrice;
            _maxPrice = MaxPrice;
        }

        protected override IEnumerable<IProduct> _FilterOut(IEnumerable<IProduct> products)
        {
            return products.Where(x => (_minPrice == null || x.Price >= _minPrice) && (_maxPrice == null || x.Price <= _maxPrice)).ToList();
        }
    }
}
