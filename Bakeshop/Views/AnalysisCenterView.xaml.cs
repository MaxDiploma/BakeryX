using Bakeshop.ViewModels;
using System;
using System.Windows;

namespace Bakeshop.Views
{

    public partial class AnalysisCenterView : Window
    {
        public AnalysisCenterView()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var viewModel = new AnalysisCenterViewModel();
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(this.Close);
        }
    }
}
