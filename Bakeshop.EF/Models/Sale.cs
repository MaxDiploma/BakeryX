using System;
using Bakeshop.Common.Enums;

namespace Bakeshop.EF.Models
{
    public class Sale : Base
    {
        public string Name { get; set; }

        public int Amount { get; set; }

        public UomTypes UomType { get; set; }

        public double Quantity { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
