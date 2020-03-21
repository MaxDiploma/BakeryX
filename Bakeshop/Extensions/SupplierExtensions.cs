using Bakeshop.DomainModels;
using Bakeshop.EF.Models;
using System.Linq;

namespace Bakeshop.Extensions
{
    public static class SupplierExtensions
    {
        public static SupplierDomain ToDomain(this Supplier supplier)
        {
            return new SupplierDomain
            {
                Age = supplier.Age,
                Email = supplier.Email,
                Firstname = supplier.Firstname,
                Lastname = supplier.Lastname,
                Phone = supplier.Phone,
                Position = supplier.Position,
                Products = supplier.Products.Select(p => p.Name).Aggregate((a, b) => a + ", " + b),
                Id = supplier.Id
            };
        }
    }
}
