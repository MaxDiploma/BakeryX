using BakeryX.ViewModels;
using System;
using System.Windows;

namespace BakeryX.Views
{
    public partial class EmployeeInformation : Window
    {
        public EmployeeInformation()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var viewModel = new EmployeeInformationViewModel();
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(this.Close);
        }
    }
}
