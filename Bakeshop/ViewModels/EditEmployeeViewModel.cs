using Bakeshop.Common.Enums;
using Bakeshop.DomainModels;
using Bakeshop.EF;
using Bakeshop.EF.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Bakeshop.ViewModels
{
    public class EditEmployeeViewModel : ObservableObject, IDisposable
    {
        private BakeshopWorker _bakeshopWorker;
        private readonly BakeshopContext _context;
        private CollectionView _positionTypes;
        private string _selectedPositionType;
        private readonly Guid _id;

        public EditEmployeeViewModel(Guid id)
        {
            _id = id;
            _context = new BakeshopContext();
            EditEmployeeCommand = new RelayCommand(EditEmployee);
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            LoadPositions();
            LoadWorker(id);
        }

        public EditEmployeeViewModel() { }

        public BakeshopWorker Employee
        {
            get { return _bakeshopWorker; }
            set { Set(ref _bakeshopWorker, value); }
        }

        public void LoadWorker(Guid id)
        {
            Employee = _context.BakeshopWorkers.FirstOrDefault(bw => bw.Id == id);
            RaisePropertyChanged("Employee");
        }
        public CollectionView PositionTypes
        {
            get { return _positionTypes; }
            set { Set(ref _positionTypes, value); }
        }

        public string SelectedPositionType
        {
            get { return _selectedPositionType; }
            set { Set(ref _selectedPositionType, value); }
        }

        public Action CloseAction { get; set; }

        public ICommand EditEmployeeCommand { get; set; }

        public ICommand GetToPreviousWindowCommand { get; set; }

        public void GetToPreviousWindow()
        {
            CloseAction();
        }

        public void EditEmployee()
        {
            if (string.IsNullOrEmpty(Employee.Age.ToString()) || string.IsNullOrEmpty(Employee.Email) || string.IsNullOrEmpty(Employee.Firstname) || string.IsNullOrEmpty(Employee.Lastname) || string.IsNullOrEmpty(Employee.Password))
            {
                MessageBox.Show("All fields must be filled.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int result;

            if (!int.TryParse(Employee.Age.ToString(), out result))
            {
                MessageBox.Show("All fields must be filled.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (result <= 18 || result > 80)
            {
                MessageBox.Show("Incorrect age.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var employee = new BakeshopWorker
            {
                Age = result,
                Email = Employee.Email,
                Firstname = Employee.Firstname,
                Lastname = Employee.Lastname,
                Password = Employee.Password,
                Phone = Employee.Phone
            };

            var employeeToEdit = _context.BakeshopWorkers.FirstOrDefault(x => x.Id == _id);
            var position = ConverStringPositionToPosition(SelectedPositionType);
            employeeToEdit.Position = position;
            _context.SaveChanges();
            CloseAction();
        }


        public void LoadPositions()
        {
            var positionsComboData = new List<ComboData>();

            positionsComboData.Add(new ComboData { Value = "Cook" });
            positionsComboData.Add(new ComboData { Value = "Trainee" });
            positionsComboData.Add(new ComboData { Value = "Manager" });
            positionsComboData.Add(new ComboData { Value = "Employee" });

            PositionTypes = new CollectionView(positionsComboData);
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
