using Bakeshop.CommandHandler;
using Bakeshop.DomainModels;
using Bakeshop.EF;
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
        private ObservableCollection<BakeryProductDomain> _bakeryProduct;
        private ICommand _searchCommand;

        public WorkstationViewModel()
        {
            _context = new BakeshopContext();
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            LoadProducts();
        }

        public ObservableCollection<BakeryProductDomain> BakeryProducts
        {
            get { return _bakeryProduct; }
            set { Set(ref _bakeryProduct, value); }
        }

        public ICommand GetToPreviousWindowCommand { get; set; }

        public ICommand PreviousPageCommand { get; set; }

        public ICommand NextPageCommand { get; set; }

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
            BakeryProducts = new ObservableCollection<BakeryProductDomain>();

            var bakeryProducts = await _context.BakeryProducts
                .ToListAsync();

            foreach (var bakeryProduct in bakeryProducts)
            {
                BakeryProducts.Add(bakeryProduct.ToDomain());
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
