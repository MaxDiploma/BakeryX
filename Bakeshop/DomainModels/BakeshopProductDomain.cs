using Bakeshop.Common.Enums;
using System;

namespace Bakeshop.DomainModels
{
    public class BakeshopProductDomain
    {
        public string Name { get; set; }

        public Guid SupplierId { get; set; }

        public int Quantity { get; set; }

        public DateTime ReceivedDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public UomTypes UomType { get; set; }

        public bool IsExpired { get; set; }
    }
}
