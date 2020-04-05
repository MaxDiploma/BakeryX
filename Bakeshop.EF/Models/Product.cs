using Bakeshop.Common.Enums;

namespace Bakeshop.EF.Models
{
    public class Product : Base
    {
        public string Name { get; set; }

        public Supplier Supplier { get; set; }

        public UomTypes Uom { get; set; }

    }
}
