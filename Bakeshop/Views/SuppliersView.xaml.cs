using Bakeshop.ViewModels;
using System;
using System.Windows;

namespace Bakeshop.Views
{
    public partial class SuppliersView : Window
    {
        public SuppliersView()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var viewModel = new SuppliersViewModel();
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(this.Close);
        }
    }
}
