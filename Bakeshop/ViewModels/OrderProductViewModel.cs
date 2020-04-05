using System.Data.Entity;
using Bakeshop.EF;
using Bakeshop.EF.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using Bakeshop.DomainModels;
using System.Windows.Data;
using GalaSoft.MvvmLight.CommandWpf;
using Bakeshop.Common.Enums;
using System.Windows;
using Bakeshop.Views;

namespace Bakeshop.ViewModels
{
    public class OrderProductViewModel : ObservableObject, IDisposable
    {
        private readonly BakeshopContext _context;
        private Supplier _supplier;
        private CollectionView _productsTypes;
        private CollectionView _uoms;
        private string _selectedProductType;
        private string _selectedUom;
        private string _quantity;

        public OrderProductViewModel(Guid? supplierId)
        {
            _context = new BakeshopContext();
            OrderCommand = new RelayCommand(OrderProducts);
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            LoadSupplier(supplierId);
        }

        public OrderProductViewModel()
        {
            _context = new BakeshopContext();
        }

        public Action CloseAction { get; set; }

        public ICommand OrderCommand { get; set; }

        public ICommand GetToPreviousWindowCommand { get; set; }

        public Supplier Supplier
        {
            get { return _supplier; }
            set { Set(ref _supplier, value); }
        }

        public string Quantity
        {
            get { return _quantity; }
            set { Set(ref _quantity, value); }
        }

        public CollectionView ProductTypes
        {
            get { return _productsTypes; }
            set { Set(ref _productsTypes, value); }
        }

        public CollectionView Uoms
        {
            get { return _uoms; }
            set
            {
                Set(ref _uoms, value);
            }
        }

        public string SelectedProductType
        {
            get { return _selectedProductType; }
            set
            {
                var uomsComboData = new List<ComboData>();
                var product = _context.Products.FirstOrDefault(p => p.Name == value);
                uomsComboData.Add(new ComboData { Value = ConvertUomToString(product.Uom) });
                Uoms = new CollectionView(uomsComboData);
                Set(ref _selectedProductType, value);
            }
        }

        public string SelectedUom
        {
            get { return _selectedUom; }
            set { Set(ref _selectedUom, value); }
        }

        public void LoadSupplier(Guid? supplierId)
        {
            if (supplierId != null)
            {
                Supplier = _context.Suppliers.Include(s => s.Products).FirstOrDefault(s => s.Id == supplierId);

                var productsComboData = new List<ComboData>();

                foreach (var product in Supplier.Products)
                {
                    productsComboData.Add(new ComboData { Id = product.Id, Value = product.Name });
                }

                ProductTypes = new CollectionView(productsComboData);
            }
        }

        public void OrderProducts()
        {
            if (string.IsNullOrEmpty(SelectedProductType) || string.IsNullOrEmpty(SelectedUom) || string.IsNullOrEmpty(Quantity))
            {
                MessageBox.Show("If you want to order something need to enter correct data", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var bakeshopProduct = new BakeshopProduct
            {
                Name = SelectedProductType,
                Quantity = int.Parse(Quantity),
                ReceivedDate = DateTime.UtcNow,
                SupplierId = Supplier.Id,
                UomType = ConverStringUomToEnum(SelectedUom),
                ExpirationDate = DateTime.UtcNow.AddDays(3)
            };

            _context.BakeshopProducts.Add(bakeshopProduct);
            _context.SaveChanges();

            MessageBox.Show("Succesfully ordered", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
            CloseAction();
        }

        private UomTypes ConverStringUomToEnum(string uomType)
        {
            switch (uomType)
            {
                case "Litres":
                    return UomTypes.Litres;
                case "Pcs":
                    return UomTypes.Pcs;
                case "Gramms":
                    return UomTypes.Gramms;
                case "Killograms":
                    return UomTypes.Killograms;
                default:
                    return UomTypes.Empty;
            }
        }

        private string ConvertUomToString(UomTypes uom)
        {
            switch (uom)
            {
                case UomTypes.Litres:
                    return "Litres";
                case UomTypes.Pcs:
                    return "Pcs";
                case UomTypes.Gramms:
                    return "Gramms";
                case UomTypes.Killograms:
                    return "Killograms";
                default:
                    return "";
            }
        }

        public void GetToPreviousWindow()
        {
            CloseAction();
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
