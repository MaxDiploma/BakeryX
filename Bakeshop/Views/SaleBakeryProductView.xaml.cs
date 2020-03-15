using Bakeshop.ViewModels;
using System;
using System.Windows;

namespace Bakeshop.Views
{
    public partial class SaleBakeryProductView : Window
    {
        public SaleBakeryProductView()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var viewModel = new SaleBakeryProductViewModel();
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(this.Close);
        }
    }
}
