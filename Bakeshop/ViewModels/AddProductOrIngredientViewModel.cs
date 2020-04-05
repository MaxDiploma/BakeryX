using Bakeshop.DomainModels;
using Bakeshop.EF;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace Bakeshop.ViewModels
{
    class AddProductOrIngredientViewModel : ObservableObject, IDisposable
    {
        private string _selectedProductName;
        private CollectionView _products;
        private BakeshopContext _context;

        public AddProductOrIngredientViewModel(Guid supplierId)
        {
            AddProductToSupplierCommand = new RelayCommand(AddProductToSupllier);
            _context = new BakeshopContext();
            LoadProducts();
        }

        public AddProductOrIngredientViewModel()
        {
            AddProductToSupplierCommand = new RelayCommand(AddProductToSupllier);
            _context = new BakeshopContext();
            LoadProducts();
        }

        public CollectionView Products
        {
            get { return _products; }
            set { Set(ref _products, value); }
        }

        public string SelectedProductName
        {
            get { return _selectedProductName; }
            set { Set(ref _selectedProductName, value); }
        }

        public ICommand AddProductToSupplierCommand { get; set; }

        public Action CloseAction { get; set; }

        public void LoadProducts()
        {
            var productsComboData = new List<ComboData>();
            var products = _context.Products.ToList();

            foreach (var product in products)
            {
                productsComboData.Add(new ComboData { Value = product.Name });
            }
            Products = new CollectionView(productsComboData);
        }
        public void AddProductToSupllier()
        {
            CloseAction();
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
