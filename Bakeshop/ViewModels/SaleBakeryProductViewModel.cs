using Bakeshop.CommandHandler;
using Bakeshop.EF;
using Bakeshop.EF.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bakeshop.ViewModels
{
    public class SaleBakeryProductViewModel : ObservableObject, IDisposable
    {
        private readonly BakeshopContext _context;
        private ObservableCollection<BakeryProduct> _bakeryProducts;
        private ICommand _saleCommand;
        private BakeryProduct _bakeryProduct;

        public SaleBakeryProductViewModel(Guid? bakeryProductId)
        {
            _context = new BakeshopContext();
            LoadBakeryProduct(bakeryProductId);
        }

        public SaleBakeryProductViewModel()
        {
            _context = new BakeshopContext();
        }

        public Action CloseAction { get; set; }

        public ICommand SaleCommand
        {
            get
            {
                return _saleCommand ?? (_saleCommand = new BaseCommandHandler(param => SaleBakeryProduct(param), true));
            }
        }
        public ObservableCollection<BakeryProduct> BakeryProducts
        {
            get { return _bakeryProducts; }
            set { Set(ref _bakeryProducts, value); }
        }

        public async void LoadBakeryProduct(Guid? bakeryProductId)
        {
            if (bakeryProductId != null)
            {
                BakeryProducts = new ObservableCollection<BakeryProduct>();

                var bakeryProduct = await _context.BakeryProducts
                    .FirstOrDefaultAsync(f => f.Id == bakeryProductId);

                _bakeryProduct = bakeryProduct;

                BakeryProducts.Add(bakeryProduct);
            }
        }

        public async void SaleBakeryProduct(object param)
        {
            var textBox = param as TextBox;
            var orderedQuantity = int.Parse(textBox.Text);

            var sale = new Sale
            {
                Amount = orderedQuantity * _bakeryProduct.Price,
                Name = _bakeryProduct.Name,
                TransactionDate = DateTime.UtcNow,
            };

            _context.Sales.Add(sale);

            var product = await _context.BakeryProducts.FirstOrDefaultAsync(bk => bk.Id == _bakeryProduct.Id);

            product.Quantity -= orderedQuantity;

            if(product.Quantity == 0)
            {
                _context.BakeryProducts.Remove(product);
            }

            _context.SaveChanges();

            CloseAction();
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
