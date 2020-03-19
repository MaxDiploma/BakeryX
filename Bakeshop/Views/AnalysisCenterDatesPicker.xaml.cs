using System;
using System.Windows;
using Bakeshop.ViewModels;

namespace Bakeshop.Views
{
    public partial class AnalysisCenterDatesPicker : Window
    {
        public AnalysisCenterDatesPicker()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var viewModel = new AnalysisCenterDatesPickerModel();
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(this.Close);
        }
    }
}
