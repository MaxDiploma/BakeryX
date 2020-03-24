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
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bakeshop.ViewModels
{
    public class WorkstationViewModel : ObservableObject, IDisposable
    {
        private BakeshopContext _context;
        private ObservableCollection<BakeryProductDomain> _bakeryProducts;
        private ObservableCollection<BakeryProduct> _orderedProducts;
        private ICommand _searchCommand;

        public WorkstationViewModel()
        {
            _context = new BakeshopContext();
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            ProcessOrderedItemsCommand = new RelayCommand(ProcessOrderedItems);
            LoadProducts();
            OrderedProducts = new ObservableCollection<BakeryProduct>();
        }

        public WorkstationViewModel(ObservableCollection<BakeryProduct> orderedProducts)
        {
            _context = new BakeshopContext();
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            ProcessOrderedItemsCommand = new RelayCommand(ProcessOrderedItems);
            LoadProducts();
            OrderedProducts = orderedProducts;
        }

        public ObservableCollection<BakeryProductDomain> BakeryProducts
        {
            get { return _bakeryProducts; }
            set { Set(ref _bakeryProducts, value); }
        }

        public ObservableCollection<BakeryProduct> OrderedProducts
        {
            get { return _orderedProducts; }
            set { Set(ref _orderedProducts, value); }
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

            var bakeryProducts = _context.BakeryProducts
                .ToList();

            foreach (var bakeryProduct in bakeryProducts)
            {
                var domainBakeryProduct = bakeryProduct.ToDomain();
                domainBakeryProduct.OnProductOrdered += UpdateOrderedProducts;

                BakeryProducts.Add(bakeryProduct.ToDomain());
            }
        }

        public void UpdateOrderedProducts(object sender, BakeryEventArgs args)
        {
            var orderedProduct = args.OrderedBakeryProduct.ToModel();
            OrderedProducts.Add(orderedProduct);

            _context.SaveChanges();

            RaisePropertyChanged("OrderedProducts");
        }

        public void ProcessOrderedItems()
        {

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
