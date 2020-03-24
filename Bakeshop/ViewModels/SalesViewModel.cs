using Bakeshop.CommandHandler;
using Bakeshop.Common.Enums;
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
    public class SalesViewModel : ObservableObject, IDisposable
    {
        private BakeshopContext _context;
        private ObservableCollection<Sale> _sales;
        private bool _isOrderedByName;
        private bool _isOrderedByAmount;
        private bool _isOrderedByDescendingPosition;
        private ICommand _searchCommand;

        public SalesViewModel()
        {
            _context = new BakeshopContext();
            SortByNameCommand = new RelayCommand(SortByName);
            SortByAmountCommand = new RelayCommand(SortByAmount);
            SortByDateCommand = new RelayCommand(SortByDate);
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            LoadSales();
        }

        public ObservableCollection<Sale> Sales
        {
            get { return _sales; }
            set { Set(ref _sales, value); }
        }

        public ICommand GetToPreviousWindowCommand { get; set; }

        public ICommand SortByNameCommand { get; set; }

        public ICommand SortByAmountCommand { get; set; }

        public ICommand SortByDateCommand { get; set; }

        public Action CloseAction { get; set; }

        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new BaseCommandHandler(param => SearchHandler(param), true));
            }
        }

        public async void LoadSales()
        {
            Sales = new ObservableCollection<Sale>();

            var sales = await _context.Sales.ToListAsync();

            foreach (var sale in sales)
            {
                Sales.Add(sale);
            }
        }

        private void SortByName()
        {
            if (_isOrderedByName)
            {
                Sales = Sales.SortObservableCollection(OrderingTypes.Ascending, x => x.Name);
                _isOrderedByName = false;
            }
            else
            {
                Sales = Sales.SortObservableCollection(OrderingTypes.Descending, x => x.Name);
                _isOrderedByName = true;
            }

            RaisePropertyChanged("Sales");
        }

        private void SortByAmount()
        {
            if (_isOrderedByAmount)
            {
                Sales = Sales.SortObservableCollection(OrderingTypes.Ascending, x => x.Amount);
                _isOrderedByAmount = false;
            }
            else
            {
                Sales = Sales.SortObservableCollection(OrderingTypes.Descending, x => x.Amount);
                _isOrderedByAmount = true;
            }

            RaisePropertyChanged("Sales");
        }

        private void SortByDate()
        {
            if (_isOrderedByDescendingPosition)
            {
                Sales = Sales.SortObservableCollection(OrderingTypes.Ascending, x => x.TransactionDate);
                _isOrderedByDescendingPosition = false;
            }
            else
            {
                Sales = Sales.SortObservableCollection(OrderingTypes.Descending, x => x.TransactionDate);
                _isOrderedByDescendingPosition = true;
            }

            RaisePropertyChanged("Sales");
        }

        private async void SearchHandler(object param)
        {
            Sales.Clear();

            var textBox = param as TextBox;
            var searchQuery = textBox.Text;

            var sales = await _context.Sales
                .Where(e => e.Name.Contains(searchQuery))
                .ToListAsync();

            foreach (var sale in sales)
            {
                Sales.Add(sale);
            }

            RaisePropertyChanged("Sales");
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
