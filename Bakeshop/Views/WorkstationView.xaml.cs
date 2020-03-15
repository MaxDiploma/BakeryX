using Bakeshop.DomainModels;
using Bakeshop.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bakeshop.Views
{
    public partial class WorkstationView : Window
    {
        public WorkstationView()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var viewModel = new WorkstationViewModel();
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(this.Close);
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            var bakeryProduct = item.Content as BakeryProductDomain;

            var saleView = new SaleBakeryProductView();
            saleView.DataContext = new SaleBakeryProductViewModel(bakeryProduct.Id as Guid?)
            {
                CloseAction = ((SaleBakeryProductViewModel)saleView.DataContext).CloseAction
            };

            saleView.ShowDialog();

            var windowContext = this.DataContext as WorkstationViewModel;
            windowContext.LoadProducts();
        }
    }
}
