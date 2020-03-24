using Bakeshop.DomainModels;
using Bakeshop.EF.Models;

namespace Bakeshop.Extensions
{
    public static class BakeryProductExtensions
    {
        public static BakeryProductDomain ToDomain(this BakeryProduct bakeryProduct)
        {
            return new BakeryProductDomain
            {
                ExpirationDate = bakeryProduct.ExpirationDate,
                Id = bakeryProduct.Id,
                IsSold = bakeryProduct.IsSold,
                Name = bakeryProduct.Name,
                Price = bakeryProduct.Price,
                Quantity = bakeryProduct.Quantity,
                UomType = bakeryProduct.UomType
            };
        }

        public static BakeryProduct ToModel(this BakeryProductDomain bakeryProduct)
        {
            return new BakeryProduct
            {
                ExpirationDate = bakeryProduct.ExpirationDate,
                Id = bakeryProduct.Id,
                IsSold = bakeryProduct.IsSold,
                Name = bakeryProduct.Name,
                Price = bakeryProduct.Price,
                Quantity = bakeryProduct.Quantity,
                UomType = bakeryProduct.UomType
            };
        }
    }
}
