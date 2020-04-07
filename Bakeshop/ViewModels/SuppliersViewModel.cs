using Bakeshop.CommandHandler;
using Bakeshop.Common.Enums;
using Bakeshop.DomainModels;
using Bakeshop.EF;
using Bakeshop.Extensions;
using Bakeshop.StaticResources;
using Bakeshop.Views;
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
    public class SuppliersViewModel : ObservableObject, IDisposable
    {
        private BakeshopContext _context;
        private ObservableCollection<SupplierDomain> _employees;
        private bool _isOrderedByDescendingFirstname;
        private bool _isOrderedByDescendingLastname;
        private bool _isOrderedByDescendingPosition;
        private Visibility _isUserAdminOrOwnerVisability;
        private ICommand _searchCommand;
        private SupplierDomain _selectedEmployee;

        public SuppliersViewModel()
        {
            _context = new BakeshopContext();
            SortByFirstnameCommand = new RelayCommand(SortByFirstname);
            SortByLastnameCommand = new RelayCommand(SortByLastName);
            SortByPositionCommand = new RelayCommand(SortByPosition);
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            AddNewSupplierCommand = new RelayCommand(AddNewSupplier);
            EditSupplierCommand = new RelayCommand(EditSupplier);

            var currentUser = CurrentUserManagment.GetCurrentUser();
            var IsAdminOrOwner = currentUser.Position == Positions.Owner || currentUser.Position == Positions.Manager ? true : false;
            IsAdminOrOwnerVisability = IsAdminOrOwner ? Visibility.Visible : Visibility.Hidden;

            LoadEmployees();
        }

        public ObservableCollection<SupplierDomain> Employees
        {
            get { return _employees; }
            set { Set(ref _employees, value); }
        }

        public SupplierDomain SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { Set(ref _selectedEmployee, value); }
        }

        public Visibility IsAdminOrOwnerVisability
        {
            get { return _isUserAdminOrOwnerVisability; }
            set { Set(ref _isUserAdminOrOwnerVisability, value); }
        }

        public ICommand GetToPreviousWindowCommand { get; set; }

        public ICommand SortByFirstnameCommand { get; set; }

        public ICommand SortByLastnameCommand { get; set; }

        public ICommand SortByPositionCommand { get; set; }

        public ICommand AddNewSupplierCommand { get; set; }

        public ICommand EditSupplierCommand { get; set; }

        public Action CloseAction { get; set; }


        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new BaseCommandHandler(param => SearchHandler(param), true));
            }
        }

        public void AddNewSupplier()
        {
            var newSupplier = new NewSupplierView();
            newSupplier.DataContext = new NewSupplierViewModel()
            {
                CloseAction = ((NewSupplierViewModel)newSupplier.DataContext).CloseAction
            };
            newSupplier.ShowDialog();
            _context = new BakeshopContext();
            LoadEmployees();
        }

        public void EditSupplier()
        {
            if (SelectedEmployee == null)
            {
                MessageBox.Show("Products already exists in the list.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var editSupplier = new EditSupplierView();
            editSupplier.DataContext = new EditSupplierViewModel(SelectedEmployee.Id)
            {
                CloseAction = ((EditSupplierViewModel)editSupplier.DataContext).CloseAction
            };
            editSupplier.ShowDialog();
            _context = new BakeshopContext();
            LoadEmployees();
        }

        public async void LoadEmployees()
        {
            Employees = new ObservableCollection<SupplierDomain>();

            var employees = await _context.Suppliers.Include(s => s.Products).ToListAsync();

            foreach (var employee in employees)
            {
                var supplier = employee.ToDomain();
                supplier.OnSupplierRemoved += ReloadSuppliers;
                Employees.Add(supplier);
            }
        }

        public void ReloadSuppliers(object sender, EventArgs e)
        {
            _context = new BakeshopContext();
            LoadEmployees();
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
