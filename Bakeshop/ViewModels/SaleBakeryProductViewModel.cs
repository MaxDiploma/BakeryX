using Bakeshop.CommandHandler;
using Bakeshop.EF;
using Bakeshop.EF.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
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
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
        }

        public SaleBakeryProductViewModel()
        {
            _context = new BakeshopContext();
        }

        public ICommand GetToPreviousWindowCommand { get; set; }

        public BakeryProduct ShopBakeryProduct { get; set; }

        public BakeryProduct OrderedBakeryProduct { get; set; }

        public bool IsNeedToProcessData { get; set; }

        public Sale Sale { get; set; }

        public Action CloseAction { get; set; }

        public int OrderedQuantity { get; set; }

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

                ShopBakeryProduct = bakeryProduct;

                _bakeryProduct = bakeryProduct;

                BakeryProducts.Add(bakeryProduct);
            }
        }

        public void GetToPreviousWindow()
        {
            CloseAction();
        }

        public void SaleBakeryProduct(object param)
        {
            var textBox = param as TextBox;
            var orderedQuantity = int.Parse(textBox.Text);
            OrderedQuantity = orderedQuantity;

            var product = _context.BakeryProducts.FirstOrDefault(bk => bk.Id == _bakeryProduct.Id);

            if (orderedQuantity > product.Quantity)
            {
                MessageBox.Show("You cannot sell more than you have", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            product.Quantity -= OrderedQuantity;

            var sale = new Sale
            {
                Amount = OrderedQuantity * product.Price,
                Name = product.Name,
                TransactionDate = DateTime.UtcNow,
                UomType = product.UomType,
                Quantity = OrderedQuantity
            };

            if (product.Quantity == 0)
            {
                _context.BakeryProducts.Remove(product);
            }

            _context.Sales.Add(sale);

            _context.SaveChanges();

            OrderedBakeryProduct = ShopBakeryProduct;
            OrderedBakeryProduct.Quantity = orderedQuantity;

            IsNeedToProcessData = true;

            CloseAction();
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
