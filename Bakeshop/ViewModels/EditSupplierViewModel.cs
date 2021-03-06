﻿using Bakeshop.Common.Enums;
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
using System.Data.Entity;
using System.Windows.Data;
using System.Windows.Input;

namespace Bakeshop.ViewModels
{
    public class EditSupplierViewModel : ObservableObject, IDisposable
    {
        private string _firstname;
        private string _lastname;
        private string _email;
        private string _password;
        private string _selectedPositionType;
        private string _phone;
        private string _age;
        private string _selectedProduct;
        private Supplier _supplier;
        private CollectionView _positionTypes;
        private BakeshopContext _context;
        private CollectionView _products;
        private Guid _supplierId;

        public EditSupplierViewModel(Guid supplierId)
        {
            _supplierId = supplierId;
            AddProductToSupplierCommand = new RelayCommand(AddProductToSupplier);
            EditSupplierCommand = new RelayCommand(EditSupplier);
            RemoveProductFromSupplierCommand = new RelayCommand(RemoveProductFromSupplier);
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            _context = new BakeshopContext();
            LoadPositions();
            LoadSupplier(supplierId);
        }

        public EditSupplierViewModel()
        {

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

        public Supplier Supplier
        {
            get { return _supplier; }
            set { Set(ref _supplier, value); }
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

        public ICommand EditSupplierCommand { get; set; }

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

            positionsComboData.Add(new ComboData { Value = "Supplier" });

            PositionTypes = new CollectionView(positionsComboData);
        }

        public void LoadSupplier(Guid supplierId)
        {
            Supplier = _context.Suppliers.Include(s => s.Products).FirstOrDefault(s => s.Id == supplierId);
            var productsComboData = new List<ComboData>();

            foreach (var product in Supplier.Products)
            {
                productsComboData.Add(new ComboData { Value = product.Name });
            }

            Products = new CollectionView(productsComboData);

            RaisePropertyChanged("Products");
        }

        public void RemoveProductFromSupplier()
        {
            var supplier = _context.Suppliers.Include(s => s.Products).FirstOrDefault(x => x.Id == _supplierId);
            var supplierProduct = supplier.Products.FirstOrDefault(x => x.Name == SelectedProduct);
            supplier.Products.Remove(supplierProduct);
            _context.SaveChanges();

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

        public void EditSupplier()
        {
            if (string.IsNullOrEmpty(Supplier.Age.ToString()) || string.IsNullOrEmpty(Supplier.Email) || string.IsNullOrEmpty(Supplier.Firstname) || string.IsNullOrEmpty(Supplier.Lastname))
            {
                MessageBox.Show("All fields must be filled.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int result;

            if (!int.TryParse(Supplier.Age.ToString(), out result))
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
                Email = Supplier.Email,
                Firstname = Supplier.Firstname,
                Lastname = Supplier.Lastname,
                Phone = Supplier.Phone,
                Products = supplierProducts,
                Position = Positions.Supplier
            };

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
