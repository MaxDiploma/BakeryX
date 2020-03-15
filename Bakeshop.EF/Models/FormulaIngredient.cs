using System;

namespace Bakeshop.EF.Models
{
    public class FormulaIngredient : Base
    {
        public Formula Formula { get; set; }

        public Guid FormulaId { get; set; }

        public Ingredient Ingredient { get; set; }

        public Guid IngredientId { get; set; }
    }
}
