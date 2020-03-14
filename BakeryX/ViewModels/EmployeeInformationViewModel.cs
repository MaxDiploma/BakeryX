using BakeryX.Commands;
using BakeryX.Common.Enums;
using BakeryX.Common.MonitorManagment;
using BakeryX.EF;
using BakeryX.EF.Models;
using BakeryX.Extensions;
using BakeryX.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BakeryX.ViewModels
{
    public class EmployeeInformationViewModel : ObservableObject, IDisposable
    {
        private int _displayWidth;
        private int _displayHeight;
        private Thickness _topFiltrationButtonMargin;
        private BakeryXContext _context;
        private ObservableCollection<Employee> _employees;
        private bool _isOrderedByDescendingFirstname;
        private bool _isOrderedByDescendingLastname;
        private bool _isOrderedByDescendingPosition;
        private ICommand _searchCommand;

        public EmployeeInformationViewModel()
        {
            _context = new BakeryXContext();
            DisplayWidth = MonitorProperties.DisplayWidth;
            DisplayHeight = MonitorProperties.DisplayHeight;
            TopFiltrationButtonMargin = new Thickness(0, DisplayHeight / 10, 0, 0);
            SortByFirstnameCommand = new RelayCommand(SortByFirstname);
            SortByLastnameCommand = new RelayCommand(SortByLastName);
            SortByPositionCommand = new RelayCommand(SortByPosition);
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            LoadEmployees();
        }

        public Thickness TopFiltrationButtonMargin
        {
            get { return _topFiltrationButtonMargin; }
            set { Set(ref _topFiltrationButtonMargin, value); }
        }

        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set { Set(ref _employees, value); }
        }

        public int DisplayWidth
        {
            get { return _displayWidth; }
            set { Set(ref _displayWidth, value); }
        }

        public int DisplayHeight
        {
            get { return _displayHeight; }
            set { Set(ref _displayHeight, value); }
        }

        public ICommand GetToPreviousWindowCommand { get; set; }

        public ICommand SortByFirstnameCommand { get; set; }

        public ICommand SortByLastnameCommand { get; set; }

        public ICommand SortByPositionCommand { get; set; }

        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new BaseCommandHandler(param => SearchHandler(param), true));
            }
        }

        public Action CloseAction { get; set; }

        public async void LoadEmployees()
        {
            Employees = new ObservableCollection<Employee>();

            var employees = await _context.Employees.ToListAsync();

            foreach (var employee in employees)
            {
                Employees.Add(employee);
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

            var employees = await _context.Employees
                .Where(e => e.Firstname.Contains(searchQuery) || e.Lastname.Contains(searchQuery))
                .ToListAsync();

            foreach(var employee in employees)
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
