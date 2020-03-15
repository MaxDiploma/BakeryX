using Bakeshop.CommandHandler;
using Bakeshop.Common.Enums;
using Bakeshop.ViewModels;
using Bakeshop.Views;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bakeshop.DomainModels
{
    public class BakeryProductDomain
    {
        private ICommand _openSaleCommand;

        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public DateTime ReceivedDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public UomTypes UomType { get; set; }

        public int Quantity { get; set; }

        public bool IsSold { get; set; }

        public ICommand OpenSaleCommand
        {
            get
            {
                return _openSaleCommand ?? (_openSaleCommand = new BaseCommandHandler(param => OpenSale(param), true));
            }
        }

        public void OpenSale(object param)
        {
            var saleView = new SaleBakeryProductView();
            saleView.DataContext = new SaleBakeryProductViewModel(param as Guid?)
            {
                CloseAction = ((RecipeDetailsViewModel)saleView.DataContext).CloseAction
            };

            saleView.ShowDialog();
        }
    }
}
