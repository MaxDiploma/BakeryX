using Bakeshop.CommandHandler;
using Bakeshop.Common.Enums;
using Bakeshop.CustomEventArgs;
using Bakeshop.EF;
using System;
using System.Linq;
using System.Windows.Input;

namespace Bakeshop.DomainModels
{
    public class BakeshopProductDomain
    {
        private ICommand _writeOffProductCommand;
        private BakeshopContext _context;

        public BakeshopProductDomain()
        {
            _context = new BakeshopContext();
        }

        public event EventHandler ProductRemoved;

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid SupplierId { get; set; }

        public double Quantity { get; set; }

        public DateTime ReceivedDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public UomTypes UomType { get; set; }

        public bool IsExpired { get; set; }

        public ICommand WriteOffProductCommand
        {
            get
            {
                return _writeOffProductCommand ?? (_writeOffProductCommand = new BaseCommandHandler(param => WriteOffProduct(param), true));
            }
        }

        public void WriteOffProduct(object bakeshopProductId)
        {
            var productId = bakeshopProductId as Guid?;
            var bakeshopProduct = _context.BakeshopProducts.FirstOrDefault(bp => bp.Id == productId);

            _context.BakeshopProducts.Remove(bakeshopProduct);
            _context.SaveChanges();

            EventHandler handler = ProductRemoved;
            handler?.Invoke(this, null);
        }
    }
}
