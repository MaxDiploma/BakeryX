using BakeryX.EF;
using System.Windows;

namespace BakeryX
{
    public partial class App : Application
    {
        private BakeryXContext _context;

        public App()
        {
            _context = new BakeryXContext();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Seed.SeedData(_context);
        }
    }
}
