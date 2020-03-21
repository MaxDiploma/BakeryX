using Bakeshop.Common.Enums;
using System.Collections.Generic;

namespace Bakeshop.EF.Models
{
    public class Ingredient : Base
    {
        public string Name { get; set; }

        public double Quantity { get; set; }

        public UomTypes UomType { get; set; }

        public ICollection<FormulaIngredient> FormulaIngredients { get; set; }
    }
}
