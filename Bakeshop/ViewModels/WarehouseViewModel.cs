using Bakeshop.CommandHandler;
using Bakeshop.Common.Enums;
using Bakeshop.DomainModels;
using Bakeshop.EF;
using Bakeshop.EF.Models;
using Bakeshop.Extensions;
using Bakeshop.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bakeshop.ViewModels
{
    public class WarehouseViewModel : ObservableObject, IDisposable
    {
        private BakeshopContext _context;
        private ObservableCollection<BakeshopProductDomain> _products;
        private bool _isOrderedByDescendingName;
        private bool _isOrderedByDescendingQuantity;
        private bool _isOrderedByDescendingDate;
        private ICommand _searchCommand;

        public WarehouseViewModel()
        {
            _context = new BakeshopContext();
            SortByNameCommand = new RelayCommand(SortByName);
            SortByQuantityCommand = new RelayCommand(SortByQuantity);
            SortByDateCommand = new RelayCommand(SortByDate);
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            LoadProducts();
        }

        public ObservableCollection<BakeshopProductDomain> Products
        {
            get { return _products; }
            set { Set(ref _products, value); }
        }

        public ICommand GetToPreviousWindowCommand { get; set; }


        public ICommand SortByNameCommand { get; set; }

        public ICommand SortByQuantityCommand { get; set; }

        public ICommand SortByDateCommand { get; set; }

        public Action CloseAction { get; set; }


        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new BaseCommandHandler(param => SearchHandler(param), true));
            }
        }

        public async void LoadProducts()
        {
            Products = new ObservableCollection<BakeshopProductDomain>();

            var products = await _context.BakeshopProducts.ToListAsync();

            foreach (var product in products)
            {
                Products.Add(product.ToDomain());
            }
        }

        private void SortByName()
        {
            if (_isOrderedByDescendingName)
            {
                Products = Products.SortObservableCollection(OrderingTypes.Ascending, x => x.Name);
                _isOrderedByDescendingName = false;
            }
            else
            {
                Products = Products.SortObservableCollection(OrderingTypes.Descending, x => x.Name);
                _isOrderedByDescendingName = true;
            }

            RaisePropertyChanged("Products");
        }

        private void SortByQuantity()
        {
            if (_isOrderedByDescendingQuantity)
            {
                Products = Products.SortObservableCollection(OrderingTypes.Ascending, x => x.Quantity);
                _isOrderedByDescendingQuantity = false;
            }
            else
            {
                Products = Products.SortObservableCollection(OrderingTypes.Descending, x => x.Quantity);
                _isOrderedByDescendingQuantity = true;
            }

            RaisePropertyChanged("Products");
        }

        private void SortByDate()
        {
            if (_isOrderedByDescendingDate)
            {
                Products = Products.SortObservableCollection(OrderingTypes.Ascending, x => x.ReceivedDate);
                _isOrderedByDescendingDate = false;
            }
            else
            {
                Products = Products.SortObservableCollection(OrderingTypes.Descending, x => x.ReceivedDate);
                _isOrderedByDescendingDate = true;
            }

            RaisePropertyChanged("Products");
        }

        private async void SearchHandler(object param)
        {
            Products.Clear();

            var textBox = param as TextBox;
            var searchQuery = textBox.Text;

            var products = await _context.BakeshopProducts
                .Where(p => p.Name.Contains(searchQuery))
                .ToListAsync();

            foreach (var product in products)
            {
                Products.Add(product.ToDomain());
            }

            RaisePropertyChanged("Products");
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
