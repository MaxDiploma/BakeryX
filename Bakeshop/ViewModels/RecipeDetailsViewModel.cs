using Bakeshop.Common.Enums;
using Bakeshop.EF;
using Bakeshop.EF.Models;
using Bakeshop.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Bakeshop.ViewModels
{
    public class RecipeDetailsViewModel : ObservableObject, IDisposable
    {
        private readonly BakeshopContext _context;
        private Formula _formula;
        private ObservableCollection<Ingredient> _ingredients;
        private bool _isCraftButtonEnabled;
        private string _quantity;
        private string _price;

        public RecipeDetailsViewModel(Guid? recipeId)
        {
            _context = new BakeshopContext();
            CraftProductCommand = new RelayCommand(Craft);
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            LoadRecipe(recipeId);
        }

        public RecipeDetailsViewModel()
        {
            _context = new BakeshopContext();
        }

        public Action CloseAction { get; set; }

        public Formula Formula
        {
            get { return _formula; }
            set { Set(ref _formula, value); }
        }

        public string Quantity
        {
            get { return _quantity; }
            set { Set(ref _quantity, value); }
        }

        public string Price
        {
            get { return _price; }
            set { Set(ref _price, value); }
        }

        public bool IsCraftButtonEnabled
        {
            get { return _isCraftButtonEnabled; }
            set { Set(ref _isCraftButtonEnabled, value); }
        }

        public ObservableCollection<Ingredient> Ingredients
        {
            get { return _ingredients; }
            set { Set(ref _ingredients, value); }
        }

        public ICommand CraftProductCommand { get; private set; }

        public ICommand GetToPreviousWindowCommand { get; private set; }

        public async void LoadRecipe(Guid? recipeId)
        {
            if (recipeId != null)
            {
                Formula = await _context.Formulas
                    .Include(f => f.FormulaIngredients.Select(fi => fi.Ingredient))
                    .FirstOrDefaultAsync(f => f.Id == recipeId);

                var ingredients = Formula.FormulaIngredients.Select(fi => fi.Ingredient);

                Ingredients = new ObservableCollection<Ingredient>();

                foreach (var item in ingredients)
                {
                    Ingredients.Add(item);
                }
            }
        }

        public void Craft()
        {
            var products = _context.BakeshopProducts.ToList();

            var ingredients = Formula.FormulaIngredients.Select(fi => fi.Ingredient);

            foreach (var ingredient in ingredients)
            {
                var product = products.FirstOrDefault(p => p.Name == ingredient.Name);

                if (product == null)
                {
                    MessageBox.Show($"There are no {ingredient.Name} found", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                switch (ingredient.UomType)
                {
                    case UomTypes.Killograms:
                    case UomTypes.Litres:
                    case UomTypes.Pcs:
                        if ((product.Quantity - ingredient.Quantity * int.Parse(Quantity)) >= 0)
                        {
                            product.Quantity -= ingredient.Quantity * int.Parse(Quantity);
                        }
                        break;
                    case UomTypes.Gramms:
                        if ((product.Quantity - (ingredient.Quantity * double.Parse(Quantity)) / 1000) >= 0)
                        {
                            var gramms = (double.Parse(Quantity) * ingredient.Quantity) / 1000;
                            product.Quantity -= gramms;
                        }
                        break;
                }
            }

            var bakeryProduct = new BakeryProduct
            {
                ExpirationDate = DateTime.UtcNow.AddDays(2),
                IsSold = true,
                Name = Formula.Name,
                Price = int.Parse(Price),
                Quantity = int.Parse(Quantity),
                UomType = UomTypes.Pcs
            };

            _context.BakeryProducts.Add(bakeryProduct);

            _context.SaveChanges();

            MessageBox.Show($"Crafted", "Exception", MessageBoxButton.OK, MessageBoxImage.Information);

            CloseAction();
        }

        public void GetToPreviousWindow()
        {
            var recipes = new RecipesView();
            recipes.Show();
            CloseAction();
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
