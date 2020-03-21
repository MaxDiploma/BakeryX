using Bakeshop.DomainModels;
using Bakeshop.EF.Models;
using Bakeshop.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            var supplier = item.Content as SupplierDomain;

            var orderView = new OrderProductView();
            orderView.DataContext = new OrderProductViewModel(supplier.Id as Guid?)
            {
                CloseAction = ((OrderProductViewModel)orderView.DataContext).CloseAction
            };

            orderView.ShowDialog();

            var viewModel = new SuppliersViewModel();
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(this.Close);
        }
    }
}
