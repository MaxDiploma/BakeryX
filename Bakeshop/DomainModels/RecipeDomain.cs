using Bakeshop.CommandHandler;
using Bakeshop.Common.Enums;
using Bakeshop.EF.Models;
using Bakeshop.ViewModels;
using Bakeshop.Views;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Bakeshop.DomainModels
{
    public class RecipeDomain
    {
        private ICommand _openRecipeDetails;

        public Guid Id { get; set; }

        public IEnumerable<FormulaIngredient> FormulaIngredients { get; set; }

        public string Name { get; set; }

        public RecipeTypes RecipeType { get; set; }

        public string Description { get; set; }

        public ICommand OpenRecipeDetailsCommand
        {
            get
            {
                return _openRecipeDetails ?? (_openRecipeDetails = new BaseCommandHandler(param => OpenRecipeDetails(param), true));
            }
        }

        public void OpenRecipeDetails(object param)
        {
            var recipeDetails = new RecipeDetailsView();
            recipeDetails.DataContext = new RecipeDetailsViewModel(param as Guid?)
            {
                CloseAction = ((RecipeDetailsViewModel)recipeDetails.DataContext).CloseAction
            };

            recipeDetails.ShowDialog();
        }
    }
}
