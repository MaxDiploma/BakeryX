using Bakeshop.EF;
using Bakeshop.EF.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace Bakeshop.ViewModels
{
    public class RecipeDetailsViewModel : ObservableObject, IDisposable
    {
        private readonly BakeshopContext _context;
        private Formula _formula;
        private ObservableCollection<Ingredient> _ingredients;

        public RecipeDetailsViewModel(Guid? recipeId)
        {
            _context = new BakeshopContext();
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

        public ObservableCollection<Ingredient> Ingredients
        {
            get { return _ingredients; }
            set { Set(ref _ingredients, value); }
        }

        public async void LoadRecipe(Guid? recipeId)
        {
            if (recipeId != null)
            {
                Formula = await _context.Formulas
                    .Include(f=>f.FormulaIngredients.Select(fi=>fi.Ingredient))
                    .FirstOrDefaultAsync(f => f.Id == recipeId);

                var ingredients = Formula.FormulaIngredients.Select(fi => fi.Ingredient);

                Ingredients = new ObservableCollection<Ingredient>();

                foreach (var item in ingredients)
                {
                    Ingredients.Add(item);
                }
            }
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
