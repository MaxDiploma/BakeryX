using Bakeshop.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bakeshop.EF.Models
{
    [Table("Formulas")]
    public class Formula : Base
    {
        public ICollection<FormulaIngredient> FormulaIngredients { get; set; }

        public string Name { get; set; }

        public RecipeTypes RecipeType { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }
    }
}
