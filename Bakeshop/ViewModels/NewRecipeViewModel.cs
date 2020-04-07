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
    public class NewRecipeViewModel : ObservableObject, IDisposable
    {
        private string _recipeDescription;
        private string _recipeName;
        private string _recipePrice;
        private string _selectedIngredient;
        private BakeshopContext _context;
        private CollectionView _ingredients;
        private List<Ingredient> _ingredientsToSave;

        public NewRecipeViewModel()
        {
            AddIngredientCommand = new RelayCommand(AddIngredient);
            AddNewRecipeCommand = new RelayCommand(AddNewRecipe);
            RemoveIngredientCommand = new RelayCommand(RemoveIngredient);
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            _ingredientsToSave = new List<Ingredient>();
            _context = new BakeshopContext();
        }

        public string RecipeDescription
        {
            get { return _recipeDescription; }
            set { Set(ref _recipeDescription, value); }
        }

        public string RecipeName
        {
            get { return _recipeName; }
            set { Set(ref _recipeName, value); }
        }

        public string RecipePrice
        {
            get { return _recipePrice; }
            set { Set(ref _recipePrice, value); }
        }

        public string SelectedIngredient
        {
            get { return _selectedIngredient; }
            set { Set(ref _selectedIngredient, value); }
        }

        public CollectionView Ingredients
        {
            get { return _ingredients; }
            set { Set(ref _ingredients, value); }
        }

        public ICommand AddNewRecipeCommand { get; set; }

        public ICommand AddIngredientCommand { get; set; }

        public ICommand RemoveIngredientCommand { get; set; }

        public ICommand GetToPreviousWindowCommand { get; set; }

        public void GetToPreviousWindow()
        {
            CloseAction();
        }

        public Action CloseAction { get; set; }

        public void RemoveIngredient()
        {
            var products = (IEnumerable<ComboData>)Ingredients.SourceCollection;
            var updateProducts = products.Where(p => p.Value != SelectedIngredient);
            var productsComboData = new List<ComboData>();

            foreach (var product in updateProducts)
            {
                productsComboData.Add(new ComboData { Value = product.Value });
            }

            Ingredients = new CollectionView(productsComboData);

            RaisePropertyChanged("Ingredients");
        }

        public void AddIngredient()
        {
            var newProduct = new AddIngredientToFomulaView();
            newProduct.DataContext = new AddIngredientToFomulaViewModel()
            {
                CloseAction = ((AddIngredientToFomulaViewModel)newProduct.DataContext).CloseAction
            };
            newProduct.ShowDialog();
            _context = new BakeshopContext();

            var castedModel = (AddIngredientToFomulaViewModel)newProduct.DataContext;

            _ingredientsToSave.Add(new Ingredient { Name = castedModel.SelectedIngredientName, Quantity = int.Parse(castedModel.Quantity), UomType = castedModel.ConvertedUom });

            ReloadProducts(castedModel.SelectedIngredientName, int.Parse(castedModel.Quantity), castedModel.ConvertedUom);
        }

        public void ReloadProducts(string selectedProductName, int quantity, UomTypes uomType)
        {
            var productsCombo = new List<ComboData>();

            productsCombo.Add(new ComboData { Value = selectedProductName + $" ,{quantity} {ConvertUomToString(uomType)}" });

            if (Ingredients?.Count > 0)
            {
                foreach (var item in Ingredients)
                {
                    var castedItem = (ComboData)item;

                    if (castedItem.Value.Split(',').FirstOrDefault() == selectedProductName)
                    {
                        MessageBox.Show("Product already exists in the list.", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    productsCombo.Add((ComboData)item);
                }
            }

            Ingredients = new CollectionView(productsCombo);
        }

        public void AddNewRecipe()
        {
            int result;

            int.TryParse(RecipePrice, out result);

            var formula = new Formula
            {
                Description = RecipeDescription,
                Name = RecipeName,
                RecipeType = RecipeTypes.Traditional,
                Price = result
            };

            formula.FormulaIngredients = new List<FormulaIngredient>();

            foreach (var ingredient in _ingredientsToSave)
            {
                formula.FormulaIngredients.Add(new FormulaIngredient
                {
                    Ingredient = ingredient
                });
            }

            _context.Formulas.Add(formula);
            _context.SaveChanges();
            CloseAction();
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
