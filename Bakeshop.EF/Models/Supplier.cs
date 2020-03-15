
using System.Collections.Generic;

namespace Bakeshop.EF.Models
{
    public class Supplier : Employee
    {
        public ICollection<Product> Products { get; set; }
    }
}
