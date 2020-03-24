using Bakeshop.CommandHandler;
using Bakeshop.Common.Enums;
using Bakeshop.CustomEventArgs;
using Bakeshop.ViewModels;
using Bakeshop.Views;
using System;
using System.Windows.Input;

namespace Bakeshop.DomainModels
{
    public class BakeryProductDomain
    {
        private ICommand _openSaleCommand;
        private ICommand _removeFomOrderCommand;
        public EventHandler<BakeryEventArgs> OnProductOrdered;
        public EventHandler<BakeryEventArgs> OnProductReturnedToWarehouse;

        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public DateTime ReceivedDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public UomTypes UomType { get; set; }

        public int Quantity { get; set; }

        public bool IsSold { get; set; }

        public bool AlwaysTrue { get; set; }

        public BakeryProductDomain Me { get { return this; } }

        public ICommand OpenSaleCommand
        {
            get
            {
                return _openSaleCommand ?? (_openSaleCommand = new BaseCommandHandler(param => OpenSale(param), true));
            }
        }

        public ICommand RemoveFromOrderCommand
        {
            get
            {
                return _removeFomOrderCommand ?? (_removeFomOrderCommand = new BaseCommandHandler(param => RemoveFromOrder(param), true));
            }
        }

        public void OpenSale(object param)
        {
            var saleView = new SaleBakeryProductView();
            saleView.DataContext = new SaleBakeryProductViewModel(param as Guid?)
            {
                CloseAction = ((SaleBakeryProductViewModel)saleView.DataContext).CloseAction
            };

            saleView.ShowDialog();
        }


        public void RemoveFromOrder(object bakeryProduct)
        {
            var handler = OnProductReturnedToWarehouse;
            handler?.Invoke(this, new BakeryEventArgs { OrderedBakeryProduct = bakeryProduct as BakeryProductDomain });
        }
    }
}
