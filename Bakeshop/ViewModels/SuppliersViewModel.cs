using Bakeshop.CommandHandler;
using Bakeshop.Common.Enums;
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
    public class SuppliersViewModel : ObservableObject, IDisposable
    {
        private BakeshopContext _context;
        private ObservableCollection<SupplierDomain> _employees;
        private bool _isOrderedByDescendingFirstname;
        private bool _isOrderedByDescendingLastname;
        private bool _isOrderedByDescendingPosition;
        private ICommand _searchCommand;

        public SuppliersViewModel()
        {
            _context = new BakeshopContext();
            SortByFirstnameCommand = new RelayCommand(SortByFirstname);
            SortByLastnameCommand = new RelayCommand(SortByLastName);
            SortByPositionCommand = new RelayCommand(SortByPosition);
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            LoadEmployees();
        }

        public ObservableCollection<SupplierDomain> Employees
        {
            get { return _employees; }
            set { Set(ref _employees, value); }
        }

        public ICommand GetToPreviousWindowCommand { get; set; }

        public ICommand SortByFirstnameCommand { get; set; }

        public ICommand SortByLastnameCommand { get; set; }

        public ICommand SortByPositionCommand { get; set; }

        public Action CloseAction { get; set; }


        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new BaseCommandHandler(param => SearchHandler(param), true));
            }
        }

        public async void LoadEmployees()
        {
            Employees = new ObservableCollection<SupplierDomain>();

            var employees = await _context.Suppliers.Include(s => s.Products).ToListAsync();

            foreach (var employee in employees)
            {
                Employees.Add(employee.ToDomain());
            }
        }

        private void SortByFirstname()
        {
            if (_isOrderedByDescendingFirstname)
            {
                Employees = Employees.SortObservableCollection(OrderingTypes.Ascending, x => x.Firstname);
                _isOrderedByDescendingFirstname = false;
            }
            else
            {
                Employees = Employees.SortObservableCollection(OrderingTypes.Descending, x => x.Firstname);
                _isOrderedByDescendingFirstname = true;
            }

            RaisePropertyChanged("Employees");
        }

        private void SortByLastName()
        {
            if (_isOrderedByDescendingLastname)
            {
                Employees = Employees.SortObservableCollection(OrderingTypes.Ascending, x => x.Lastname);
                _isOrderedByDescendingLastname = false;
            }
            else
            {
                Employees = Employees.SortObservableCollection(OrderingTypes.Descending, x => x.Lastname);
                _isOrderedByDescendingLastname = true;
            }

            RaisePropertyChanged("Employees");
        }

        private void SortByPosition()
        {
            if (_isOrderedByDescendingPosition)
            {
                Employees = Employees.SortObservableCollection(OrderingTypes.Ascending, x => x.Position);
                _isOrderedByDescendingPosition = false;
            }
            else
            {
                Employees = Employees.SortObservableCollection(OrderingTypes.Descending, x => x.Position);
                _isOrderedByDescendingPosition = true;
            }

            RaisePropertyChanged("Employees");
        }

        private async void SearchHandler(object param)
        {
            Employees.Clear();

            var textBox = param as TextBox;
            var searchQuery = textBox.Text;

            var employees = await _context.Suppliers
                .Where(e => e.Firstname.Contains(searchQuery) || e.Lastname.Contains(searchQuery))
                .ToListAsync();

            foreach (var employee in employees)
            {
                Employees.Add(employee.ToDomain());
            }

            RaisePropertyChanged("Employees");
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
