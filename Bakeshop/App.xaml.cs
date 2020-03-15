using Bakeshop.EF;
using System.Windows;

namespace Bakeshop
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            BakeshopDbInitializer.Seed();
        }
    }
}
