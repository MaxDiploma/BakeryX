using Bakeshop.DomainModels;
using System;

namespace Bakeshop.CustomEventArgs
{
    public class BakeryEventArgs : EventArgs
    {
        public BakeryProductDomain OrderedBakeryProduct { get; set; }

        public int OrderedQuantity { get; set; }
    }
}
