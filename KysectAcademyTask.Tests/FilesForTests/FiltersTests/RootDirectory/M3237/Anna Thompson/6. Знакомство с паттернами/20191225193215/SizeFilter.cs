using LABA_6__Filter_.API.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LABA_6__Filter_.API.Interfaces;

namespace LABA_6__Filter_.Filters
{
    public class SizeFilter : Filter
    {
        private int? _minSize { get; set; }
        private int? _maxSize { get; set; }

        public SizeFilter(int? MinSize = null, int? MaxSize = null)
        {
            _minSize = MinSize;
            _maxSize = MaxSize;
        }

        protected override IEnumerable<IProduct> _FilterOut(IEnumerable<IProduct> products)
        {
            return products.Where(x => (_minSize == null || x.Size >= _minSize) && (_maxSize == null || x.Size <= _maxSize)).ToList();
        }
    }
}
