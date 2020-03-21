using Bakeshop.Common.Enums;
using System;

namespace Bakeshop.EF.Models
{
    public class BakeryProduct : Base
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public DateTime ExpirationDate { get; set; }

        public UomTypes UomType { get; set; }

        public int Quantity { get; set; }

        public bool IsSold { get; set; }
    }
}
