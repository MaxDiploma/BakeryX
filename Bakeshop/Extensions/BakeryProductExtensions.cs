using Bakeshop.DomainModels;
using Bakeshop.EF.Models;
using System.Collections.ObjectModel;
using System.Linq;

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

        public static ObservableCollection<BakeryProductDomain> ToDomainObservableCollection(this ObservableCollection<BakeryProduct> bakeryProducts)
        {
            var updatedItems = bakeryProducts.Select(x => x.ToDomain());

            var colletion = new ObservableCollection<BakeryProductDomain>();

            foreach(var item in updatedItems)
            {
                colletion.Add(item);
            }

            return colletion;
        }

        public static ObservableCollection<BakeryProduct> ToModelObservableCollection(this ObservableCollection<BakeryProductDomain> bakeryProducts)
        {
            var updatedItems = bakeryProducts.Select(x => x.ToModel());

            var colletion = new ObservableCollection<BakeryProduct>();

            foreach (var item in updatedItems)
            {
                colletion.Add(item);
            }

            return colletion;
        }

    }
}
