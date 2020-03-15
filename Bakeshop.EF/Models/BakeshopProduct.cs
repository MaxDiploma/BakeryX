using Bakeshop.Common.Enums;
using System;

namespace Bakeshop.EF.Models
{
    public class BakeshopProduct : Base
    {
        public string Name { get; set; }

        public Guid SupplierId { get; set; }

        public int Quantity { get; set; }

        public DateTime ReceivedDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public UomTypes UomType { get; set; }
    }
}
