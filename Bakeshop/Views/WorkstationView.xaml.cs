using Bakeshop.CustomEventArgs;
using Bakeshop.DomainModels;
using Bakeshop.Extensions;
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

            bakeryProduct.OnProductOrdered = ((WorkstationViewModel)this.DataContext).UpdateOrderedProducts;

            var saleView = new SaleBakeryProductView();
            saleView.DataContext = new SaleBakeryProductViewModel(bakeryProduct.Id as Guid?)
            {
                CloseAction = ((SaleBakeryProductViewModel)saleView.DataContext).CloseAction
            };

            saleView.ShowDialog();

            if (((SaleBakeryProductViewModel)saleView.DataContext).IsNeedToProcessData)
            {
                bakeryProduct.OnProductOrdered?.Invoke(this, new BakeryEventArgs { OrderedBakeryProduct = ((SaleBakeryProductViewModel)saleView.DataContext).OrderedBakeryProduct.ToDomain() });

                var viewModel = new WorkstationViewModel(((WorkstationViewModel)this.DataContext).OrderedProducts);
                this.DataContext = viewModel;
                if (viewModel.CloseAction == null)
                    viewModel.CloseAction = new Action(this.Close);

                ((SaleBakeryProductViewModel)saleView.DataContext).IsNeedToProcessData = false;
            }
        }
    }
}
