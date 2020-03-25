using Bakeshop.Common.Enums;
using Bakeshop.DomainModels;
using Bakeshop.EF;
using Bakeshop.EF.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Bakeshop.ViewModels
{
    public class NewEmployeeViewModel : ObservableObject, IDisposable
    {
        private string _firstname;
        private string _lastname;
        private string _email;
        private string _password;
        private string _selectedPositionType;
        private string _phone;
        private int _age;
        private CollectionView _positionTypes;
        private BakeshopContext _context;

        public NewEmployeeViewModel()
        {
            AddNewEmployeeCommand = new RelayCommand(AddEmployee);
            _context = new BakeshopContext();
            LoadPositions();
        }

        public string Firstname
        {
            get { return _firstname; }
            set { Set(ref _firstname, value); }
        }

        public string Lastname
        {
            get { return _lastname; }
            set { Set(ref _lastname, value); }
        }

        public string Email
        {
            get { return _email; }
            set { Set(ref _email, value); }
        }

        public string Password
        {
            get { return _password; }
            set { Set(ref _password, value); }
        }

        public string SelectedPositionType
        {
            get { return _selectedPositionType; }
            set { Set(ref _selectedPositionType, value); }
        }

        public string Age
        {
            get { return _selectedPositionType; }
            set { Set(ref _selectedPositionType, value); }
        }

        public string Phone
        {
            get { return _phone; }
            set { Set(ref _phone, value); }
        }

        public CollectionView PositionTypes
        {
            get { return _positionTypes; }
            set { Set(ref _positionTypes, value); }
        }

        public ICommand AddNewEmployeeCommand { get; set; }

        public Action CloseAction { get; set; }

        public void LoadPositions()
        {
            var positionsComboData = new List<ComboData>();

            positionsComboData.Add(new ComboData { Value = "Cook" });
            positionsComboData.Add(new ComboData { Value = "Trainee" });
            positionsComboData.Add(new ComboData { Value = "Manager" });
            positionsComboData.Add(new ComboData { Value = "Employee" });

            PositionTypes = new CollectionView(positionsComboData);
        }

        public void AddEmployee()
        {
            if (string.IsNullOrEmpty(Age) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Firstname) || string.IsNullOrEmpty(Lastname) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("All fields must be filled.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int result;

            if (int.TryParse(Age, out result))
            {
                MessageBox.Show("All fields must be filled.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var employee = new BakeshopWorker
            {
                Age = result,
                Email = Email,
                Firstname = Firstname,
                Lastname = Lastname,
                Password = Password,
                Position = ConverStringPositionToPosition(SelectedPositionType)
            };

            _context.BakeshopWorkers.Add(employee);

            _context.SaveChanges();

            CloseAction();
        }

        private Positions ConverStringPositionToPosition(string position)
        {
            switch (position)
            {
                case "Cook":
                    return Positions.Cook;
                case "Trainee":
                    return Positions.Trainee;
                case "Manager":
                    return Positions.Manager;
                case "Employee":
                    return Positions.Employee;
                default: return Positions.Employee;
            }
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
