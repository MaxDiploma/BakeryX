using Bakeshop.CommandHandler;
using Bakeshop.CustomEventArgs;
using Bakeshop.DomainModels;
using Bakeshop.EF;
using Bakeshop.EF.Models;
using Bakeshop.Extensions;
using Bakeshop.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bakeshop.ViewModels
{
    public class WorkstationViewModel : ObservableObject, IDisposable
    {
        private BakeshopContext _context;
        private ObservableCollection<BakeryProductDomain> _bakeryProducts;
        private ObservableCollection<BakeryProductDomain> _orderedProducts;
        private ICommand _searchCommand;
        private BakeryProductDomain _selectedToReturn;

        public WorkstationViewModel()
        {
            _context = new BakeshopContext();
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            ProcessOrderedItemsCommand = new RelayCommand(ProcessOrderedItems);
            LoadProducts();
            OrderedProducts = new ObservableCollection<BakeryProductDomain>();
        }

        public WorkstationViewModel(ObservableCollection<BakeryProductDomain> orderedProducts)
        {
            _context = new BakeshopContext();
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            ProcessOrderedItemsCommand = new RelayCommand(ProcessOrderedItems);
            LoadProducts();
            foreach (var product in orderedProducts)
            {
                product.OnProductReturnedToWarehouse += ReturnToWarehouse;
            }
            OrderedProducts = orderedProducts;
        }

        public ObservableCollection<BakeryProductDomain> BakeryProducts
        {
            get { return _bakeryProducts; }
            set { Set(ref _bakeryProducts, value); }
        }

        public ObservableCollection<BakeryProductDomain> OrderedProducts
        {
            get { return _orderedProducts; }
            set { Set(ref _orderedProducts, value); }
        }

        public BakeryProductDomain SelectedToReturn
        {
            get { return _selectedToReturn; }
            set { Set(ref _selectedToReturn, value); }
        }

        public ICommand GetToPreviousWindowCommand { get; set; }

        public ICommand PreviousPageCommand { get; set; }

        public ICommand NextPageCommand { get; set; }

        public ICommand ProcessOrderedItemsCommand { get; set; }

        public Action CloseAction { get; set; }

        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new BaseCommandHandler(param => SearchHandler(param), true));
            }
        }

        public void LoadProducts()
        {
            BakeryProducts = new ObservableCollection<BakeryProductDomain>();
            _context = new BakeshopContext();

            var bakeryProducts = _context.BakeryProducts
                .ToList();

            foreach (var bakeryProduct in bakeryProducts)
            {
                var domainBakeryProduct = bakeryProduct.ToDomain();
                domainBakeryProduct.OnProductOrdered += UpdateOrderedProducts;

                BakeryProducts.Add(bakeryProduct.ToDomain());
            }
        }

        public void ReturnToWarehouse(object sender, BakeryEventArgs args)
        {
            var bakeryProduct = _context.BakeryProducts.FirstOrDefault(bp => bp.Name == args.OrderedBakeryProduct.Name);
            if (bakeryProduct == null)
            {
                var newBakeryProduct = args.OrderedBakeryProduct.ToModel();
                _context.BakeryProducts.Add(newBakeryProduct);
            }
            else
            {
                bakeryProduct.Quantity += args.OrderedBakeryProduct.Quantity;
            }
            _context.SaveChanges();

            var bakeryProductView = BakeryProducts.FirstOrDefault(bp => bp.Name == args.OrderedBakeryProduct.Name);

            if (bakeryProductView == null)
            {
                BakeryProducts.Add(args.OrderedBakeryProduct);
            }
            else
            {
                bakeryProductView.Quantity += args.OrderedBakeryProduct.Quantity;
            }

            var orderedItem = OrderedProducts.FirstOrDefault(bp => bp.Name == args.OrderedBakeryProduct.Name);
            OrderedProducts.Remove(orderedItem);

            RaisePropertyChanged("BakeryProducts");
            RaisePropertyChanged("OrderedProducts");

            LoadProducts();
        }

        public void UpdateOrderedProducts(object sender, BakeryEventArgs args)
        {
            var orderedProduct = args.OrderedBakeryProduct;
            orderedProduct.AlwaysTrue = true;
            OrderedProducts.Add(orderedProduct);
            RaisePropertyChanged("OrderedProducts");
        }

        public void ProcessOrderedItems()
        {
            if (OrderedProducts.Any())
            {
                var sales = new List<Sale>();

                foreach(var item in OrderedProducts)
                {
                    var sale = new Sale
                    {
                        Amount = item.Quantity * item.Price,
                        Name = item.Name,
                        TransactionDate = DateTime.UtcNow,
                        UomType = item.UomType,
                        Quantity = item.Quantity
                    };

                    sales.Add(sale);
                }

                _context.SaveChanges();

                var receiptView = new ReceiptView();
                var viewModel = new ReceiptViewModel();
                receiptView.DataContext = viewModel;
                if (viewModel.CloseAction == null)
                    viewModel.CloseAction = new Action(receiptView.Close);

                ((ReceiptViewModel)receiptView.DataContext).LoadOrderedProducts(OrderedProducts.ToModelObservableCollection());
                receiptView.ShowDialog();

                OrderedProducts.Clear();
                RaisePropertyChanged("OrderedProducts");
            }
            else
            {
                MessageBox.Show("There is no item to order", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        private async void SearchHandler(object param)
        {
            BakeryProducts.Clear();

            var textBox = param as TextBox;
            var searchQuery = textBox.Text;

            var bakeryProducts = await _context.BakeryProducts
                .Where(p => p.Name.Contains(searchQuery))
                .ToListAsync();

            foreach (var bakeryProduct in bakeryProducts)
            {
                BakeryProducts.Add(bakeryProduct.ToDomain());
            }

            RaisePropertyChanged("BakeryProducts");
        }

        public void GetToPreviousWindow()
        {
            var menu = new MenuView();
            menu.Show();
            CloseAction();
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
