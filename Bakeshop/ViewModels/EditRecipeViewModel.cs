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
using System.Data.Entity;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Bakeshop.ViewModels
{
    public class EditRecipeViewModel : ObservableObject, IDisposable
    {
        private string _recipeDescription;
        private string _recipeName;
        private string _recipePrice;
        private string _selectedIngredient;
        private BakeshopContext _context;
        private CollectionView _ingredients;
        private List<Ingredient> _ingredientsToSave;
        private Formula _formula;
        private Guid _formulaId;

        public EditRecipeViewModel()
        {
            AddIngredientCommand = new RelayCommand(AddIngredient);
            EditRecipeCommand = new RelayCommand(EditRecipe);
            RemoveIngredientCommand = new RelayCommand(RemoveIngredient);
            _ingredientsToSave = new List<Ingredient>();
            _context = new BakeshopContext();
        }

        public EditRecipeViewModel(Guid recipeId)
        {
            _formulaId = recipeId;
            AddIngredientCommand = new RelayCommand(AddIngredient);
            EditRecipeCommand = new RelayCommand(EditRecipe);
            RemoveIngredientCommand = new RelayCommand(RemoveIngredient);
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            _ingredientsToSave = new List<Ingredient>();
            _context = new BakeshopContext();
            LoadRecipe(recipeId);
        }

        public string RecipeDescription
        {
            get { return _recipeDescription; }
            set { Set(ref _recipeDescription, value); }
        }

        public Formula Formula
        {
            get { return _formula; }
            set { Set(ref _formula, value); }
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

        public ICommand EditRecipeCommand { get; set; }

        public ICommand AddIngredientCommand { get; set; }

        public ICommand RemoveIngredientCommand { get; set; }

        public ICommand GetToPreviousWindowCommand { get; set; }

        public Action CloseAction { get; set; }

        public void GetToPreviousWindow()
        {
            CloseAction();
        }

        public void RemoveIngredient()
        {
            var products = (IEnumerable<ComboData>)Ingredients.SourceCollection;
            var updateProducts = products.Where(p => p.Value != SelectedIngredient);
            var productsComboData = new List<ComboData>();
            var ingredientName = SelectedIngredient.Split(',').FirstOrDefault().Trim();

            if (_ingredientsToSave.Count > 0)
            {
                var ingredientToSave = _ingredientsToSave.FirstOrDefault(its => its.Name == ingredientName);
                _ingredientsToSave.Remove(ingredientToSave);
            }

            if (Formula.FormulaIngredients.Count > 0)
            {
                var formulaToDelete = Formula.FormulaIngredients.Where(fi => fi.Ingredient.Name == ingredientName).ToList();
                _context.FormulaIngredients.RemoveRange(formulaToDelete);
            }

            if (updateProducts.Count() == 1)
            {
                MessageBox.Show("You cannot remove all of the products. ", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var formulaToChange = _context.Formulas.Include(f => f.FormulaIngredients.Select(fi => fi.Ingredient)).FirstOrDefault(f => f.Id == _formulaId);

            foreach (var product in updateProducts)
            {
                productsComboData.Add(new ComboData { Value = product.Value });
            }

            Ingredients = new CollectionView(productsComboData);

            RaisePropertyChanged("Ingredients");
        }

        public void LoadRecipe(Guid id)
        {
            Formula = _context.Formulas.Include(f => f.FormulaIngredients.Select(fi => fi.Ingredient)).FirstOrDefault(f => f.Id == id);
            var ingredients = Formula.FormulaIngredients.Select(fi => fi.Ingredient);
            var ingredientsComboData = new List<ComboData>();

            foreach (var ingredient in ingredients)
            {
                ingredientsComboData.Add(new ComboData { Value = ingredient.Name + $" ,{ingredient.Quantity} {ConvertUomToString(ingredient.UomType)}" });
            }

            Ingredients = new CollectionView(ingredientsComboData);
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

        public void EditRecipe()
        {
            int result;

            int.TryParse(Formula.Price.ToString(), out result);

            var formula = new Formula
            {
                Description = Formula.Description,
                Name = Formula.Name,
                RecipeType = RecipeTypes.Traditional,
                Price = result
            };

            Formula.Description = formula.Description;
            Formula.Name = formula.Name;
            Formula.Price = formula.Price;

            foreach (var ingredient in _ingredientsToSave)
            {
                Formula.FormulaIngredients.Add(new FormulaIngredient
                {
                    Ingredient = ingredient
                });
            }

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
