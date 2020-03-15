using Bakeshop.DomainModels;
using Bakeshop.EF.Models;

namespace Bakeshop.Extensions
{
    public static class RecipeExtensions
    {
        public static RecipeDomain ToDomain(this Formula formula)
        {
            return new RecipeDomain
            {
                Id = formula.Id,
                Description = formula.Description,
                FormulaIngredients = formula.FormulaIngredients,
                Name = formula.Name,
                RecipeType = formula.RecipeType
            };
        }
    }
}
