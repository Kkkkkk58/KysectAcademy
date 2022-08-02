using LABA_6__Filter_.API.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LABA_6__Filter_.API.Interfaces;

namespace LABA_6__Filter_.Filters
{
    public class ColorFilter : Filter
    {
        private Color _color { get; set; }
        public ColorFilter(Color Color) : base() 
        {
            _color = Color;
        }

        protected override IEnumerable<IProduct> _FilterOut(IEnumerable<IProduct> products)
        {
            return products.Where(x => x.Color == this._color).ToList();
        }
    }
}
