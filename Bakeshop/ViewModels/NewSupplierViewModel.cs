using Bakeshop.Common.Enums;
using Bakeshop.DomainModels;
using Bakeshop.EF;
using Bakeshop.EF.Models;
using Bakeshop.Views;
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
    public class NewSupplierViewModel : ObservableObject, IDisposable
    {
        private string _firstname;
        private string _lastname;
        private string _email;
        private string _password;
        private string _selectedPositionType;
        private string _phone;
        private string _age;
        private string _selectedProduct;
        private CollectionView _positionTypes;
        private BakeshopContext _context;
        private CollectionView _products;

        public NewSupplierViewModel()
        {
            AddProductToSupplierCommand = new RelayCommand(AddProductToSupplier);
            AddNewSupplierCommand = new RelayCommand(AddSupplier);
            RemoveProductFromSupplierCommand = new RelayCommand(RemoveProductFromSupplier);
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            _context = new BakeshopContext();
            LoadPositions();
        }

        public string Firstname
        {
            get { return _firstname; }
            set { Set(ref _firstname, value); }
        }

        public string SelectedProduct
        {
            get { return _selectedProduct; }
            set { Set(ref _selectedProduct, value); }
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
            get { return _age; }
            set { Set(ref _age, value); }
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

        public CollectionView Products
        {
            get { return _products; }
            set { Set(ref _products, value); }
        }

        public ICommand AddNewSupplierCommand { get; set; }

        public ICommand AddProductToSupplierCommand { get; set; }

        public ICommand RemoveProductFromSupplierCommand { get; set; }

        public ICommand GetToPreviousWindowCommand { get; set; }

        public void GetToPreviousWindow()
        {
            CloseAction();
        }

        public Action CloseAction { get; set; }

        public void LoadPositions()
        {
            var positionsComboData = new List<ComboData>();

            positionsComboData.Add(new ComboData { Value = "Employee" });

            PositionTypes = new CollectionView(positionsComboData);
        }

        public void RemoveProductFromSupplier()
        {
            var products = (IEnumerable<ComboData>)Products.SourceCollection;
            var updateProducts = products.Where(p => p.Value != SelectedProduct);
            var productsComboData = new List<ComboData>();

            foreach (var product in updateProducts)
            {
                productsComboData.Add(new ComboData { Value = product.Value });
            }

            Products = new CollectionView(productsComboData);

            RaisePropertyChanged("Products");
        }

        public void AddProductToSupplier()
        {
            var newProduct = new AddProductOrIngredientView();
            newProduct.DataContext = new AddProductOrIngredientViewModel()
            {
                CloseAction = ((AddProductOrIngredientViewModel)newProduct.DataContext).CloseAction
            };
            newProduct.ShowDialog();
            _context = new BakeshopContext();
            ReloadProducts(((AddProductOrIngredientViewModel)newProduct.DataContext).SelectedProductName);
        }

        public void ReloadProducts(string selectedProductName)
        {
            var productsCombo = new List<ComboData>();

            productsCombo.Add(new ComboData { Value = selectedProductName });

            if (Products?.Count > 0)
            {
                foreach (var item in Products)
                {
                    var castedItem = (ComboData)item;

                    if (castedItem.Value == selectedProductName)
                    {
                        MessageBox.Show("Products already exists in the list.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    productsCombo.Add((ComboData)item);
                }
            }

            Products = new CollectionView(productsCombo);
        }

        public void AddSupplier()
        {
            if (string.IsNullOrEmpty(Age) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Firstname) || string.IsNullOrEmpty(Lastname))
            {
                MessageBox.Show("All fields must be filled.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int result;

            if (!int.TryParse(Age, out result))
            {
                MessageBox.Show("All fields must be filled.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (result <= 18 || result > 80)
            {
                MessageBox.Show("Incorrect age.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var supplierProducts = new List<Product>();

            foreach (var item in Products)
            {
                var castedItem = (ComboData)item;
                var product = _context.Products.FirstOrDefault(p => p.Name == castedItem.Value);

                supplierProducts.Add(product);
            }

            var employee = new Supplier
            {
                Age = result,
                Email = Email,
                Firstname = Firstname,
                Lastname = Lastname,
                Phone = Phone,
                Position = ConverStringPositionToPosition(SelectedPositionType),
            };

            employee.Products = new List<Product>();

            foreach (var supplierProduct in supplierProducts)
            {
                employee.Products.Add(supplierProduct);
            }

            _context.Suppliers.Add(employee);

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

