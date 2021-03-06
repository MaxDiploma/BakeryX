﻿using Bakeshop.CommandHandler;
using Bakeshop.Common.Enums;
using Bakeshop.DomainModels;
using Bakeshop.EF;
using Bakeshop.EF.Models;
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
    public class EmployeeViewModel : ObservableObject, IDisposable
    {
        private BakeshopContext _context;
        private ObservableCollection<EmployeeDomain> _employees;
        private EmployeeDomain _selectedEmployee;
        private bool _isOrderedByDescendingFirstname;
        private bool _isOrderedByDescendingLastname;
        private bool _isOrderedByDescendingPosition;
        private ICommand _searchCommand;
        private Visibility _isUserAdminOrOwnerVisability;

        public EmployeeViewModel()
        {
            _context = new BakeshopContext();
            SortByFirstnameCommand = new RelayCommand(SortByFirstname);
            SortByLastnameCommand = new RelayCommand(SortByLastName);
            SortByPositionCommand = new RelayCommand(SortByPosition);
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            AddNewEmployeeCommand = new RelayCommand(AddNewEmployee);
            EditEmployeeCommand = new RelayCommand(EditEmployee);

            CurrentUser = CurrentUserManagment.GetCurrentUser();
            var IsAdminOrOwner = CurrentUser.Position == Positions.Owner || CurrentUser.Position == Positions.Manager ? true : false;
            IsAdminOrOwnerVisability = IsAdminOrOwner ? Visibility.Visible : Visibility.Hidden;

            LoadEmployees();
        }

        public ObservableCollection<EmployeeDomain> Employees
        {
            get { return _employees; }
            set { Set(ref _employees, value); }
        }

        public EmployeeDomain SelectedEmployee
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

        public ICommand AddNewEmployeeCommand { get; set; }

        public ICommand EditEmployeeCommand { get; set; }

        public Action CloseAction { get; set; }

        public BakeshopWorker CurrentUser { get; set; }

        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new BaseCommandHandler(param => SearchHandler(param), true));
            }
        }

        public async void LoadEmployees()
        {
            Employees = new ObservableCollection<EmployeeDomain>();

            var employees = await _context.BakeshopWorkers.ToListAsync();

            var domainEmployees = employees.Select(e => e.ToDomain());

            foreach (var employee in domainEmployees)
            {
                employee.OnEmployeeRemoved += ReloadEmployees;
                Employees.Add(employee);
            }
        }

        public void ReloadEmployees(object sender, EventArgs e)
        {
            LoadEmployees();
        }

        public void AddNewEmployee()
        {
            var newEmployee = new NewEmployeeView();
            newEmployee.ShowDialog();
            _context = new BakeshopContext();
            LoadEmployees();
        }

        public void EditEmployee()
        {
            var editEmployee = new EditEmployeeView();
            editEmployee.DataContext = new EditEmployeeViewModel(SelectedEmployee.Id)
            {
                CloseAction = ((EditEmployeeViewModel)editEmployee.DataContext).CloseAction
            };
            editEmployee.ShowDialog();
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

            var employees = await _context.BakeshopWorkers
                .Where(e => e.Firstname.Contains(searchQuery) || e.Lastname.Contains(searchQuery))
                .Select(e => e.ToDomain())
                .ToListAsync();

            foreach (var employee in employees)
            {
                Employees.Add(employee);
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
