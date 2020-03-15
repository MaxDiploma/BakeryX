using Bakeshop.ViewModels;
using System;
using System.Windows;

namespace Bakeshop.Views
{
    public partial class RecipesView : Window
    {
        public RecipesView()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var viewModel = new RecipesViewModel();
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(this.Close);
        }
    }
}
