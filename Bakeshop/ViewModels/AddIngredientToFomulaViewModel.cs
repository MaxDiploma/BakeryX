using Bakeshop.Common.Enums;
using Bakeshop.DomainModels;
using Bakeshop.EF;
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
    public class AddIngredientToFomulaViewModel : ObservableObject, IDisposable
    {
        private string _selectedIngredientName;
        private string _selectedUom;
        private string _quantity;
        private CollectionView _products;
        private CollectionView _uoms;
        private BakeshopContext _context;

        public AddIngredientToFomulaViewModel(Guid supplierId)
        {
            AddIngredientToRecipeCommand = new RelayCommand(AddIngredientToRecipe);
            _context = new BakeshopContext();
            LoadProducts();
        }

        public AddIngredientToFomulaViewModel()
        {
            AddIngredientToRecipeCommand = new RelayCommand(AddIngredientToRecipe);
            _context = new BakeshopContext();
            LoadProducts();
            LoadUoms();
        }

        public CollectionView Products
        {
            get { return _products; }
            set { Set(ref _products, value); }
        }

        public CollectionView Uoms
        {
            get { return _uoms; }
            set { Set(ref _uoms, value); }
        }

        public string SelectedIngredientName
        {
            get { return _selectedIngredientName; }
            set { Set(ref _selectedIngredientName, value); }
        }
        public string SelectedUom
        {
            get { return _selectedUom; }
            set { Set(ref _selectedUom, value); }
        }


        public UomTypes ConvertedUom { get; set; }

        public string Quantity
        {
            get { return _quantity; }
            set { Set(ref _quantity, value); }
        }

        public ICommand AddIngredientToRecipeCommand { get; set; }

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

        public void LoadUoms()
        {
            var uomsComboData = new List<ComboData>();
            var uoms = new List<string>
            {
                  "Killograms",
                  "Pcs",
                  "Litres",
                  "Gramms",
            };

            foreach (var name in uoms)
            {
                uomsComboData.Add(new ComboData { Value = name });
            }
            Uoms = new CollectionView(uomsComboData);
        }

        public void AddIngredientToRecipe()
        {
            int resultQuantity;

            if (!int.TryParse(Quantity, out resultQuantity))
            {
                MessageBox.Show("Enter valid quantity.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(SelectedUom) || string.IsNullOrEmpty(SelectedIngredientName))
            {
                MessageBox.Show("Fill all of the fields.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ConvertedUom = ConverStringUomToEnum(SelectedUom);
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

        public void Dispose()
        {
            Dispose();
        }
    }
}
