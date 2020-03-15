using Bakeshop.ViewModels;
using System;
using System.Windows;

namespace Bakeshop.Views
{
    public partial class EmployeeView : Window
    {
        public EmployeeView()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var viewModel = new EmployeeViewModel();
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(this.Close);
        }
    }
}
