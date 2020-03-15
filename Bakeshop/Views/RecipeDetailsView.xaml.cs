using Bakeshop.ViewModels;
using System;
using System.Windows;

namespace Bakeshop.Views
{
    public partial class RecipeDetailsView : Window
    {
        public RecipeDetailsView()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var viewModel = new RecipeDetailsViewModel();
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(this.Close);
        }
    }
}
