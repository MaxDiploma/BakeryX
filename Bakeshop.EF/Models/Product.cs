using System.Collections.Generic;

namespace Bakeshop.EF.Models
{
    public class Product : Base
    {
        public string Name { get; set; }

        public Supplier Supplier { get; set; }

    }
}
