using BakeryX.EF.Models;
using System.Data.Entity;

namespace BakeryX.EF
{
    public class BakeryXContext : DbContext
    {
        public BakeryXContext() : base("BakeryXDB")
        {

        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Products> Products { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Sale> Sales { get; set; }

    }
}
