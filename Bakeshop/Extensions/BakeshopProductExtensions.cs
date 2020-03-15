using Bakeshop.DomainModels;
using Bakeshop.EF.Models;
using System;

namespace Bakeshop.Extensions
{
    public static class BakeshopProductExtensions
    {
        public static BakeshopProductDomain ToDomain(this BakeshopProduct bakeshopProduct)
        {
            return new BakeshopProductDomain
            {
                ExpirationDate = bakeshopProduct.ExpirationDate,
                ReceivedDate = bakeshopProduct.ReceivedDate,
                Quantity = bakeshopProduct.Quantity,
                Name = bakeshopProduct.Name,
                IsExpired = bakeshopProduct.ExpirationDate > DateTime.UtcNow ? true : false,
                UomType = bakeshopProduct.UomType
            };
        }
    }
}
