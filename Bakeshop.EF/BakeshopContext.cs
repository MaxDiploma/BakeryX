using Bakeshop.EF.Models;
using System.Data.Entity;

namespace Bakeshop.EF
{
    public class BakeshopContext : DbContext
    {
        public BakeshopContext() : base("Bakeshop")
        {
        }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<BakeshopWorker> BakeshopWorkers { get; set; }

        public DbSet<Formula> Formulas { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<BakeshopProduct> BakeshopProducts { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<BakeryProduct> BakeryProducts { get; set; }

        public DbSet<Sale> Sales { get; set; }
    }
}
