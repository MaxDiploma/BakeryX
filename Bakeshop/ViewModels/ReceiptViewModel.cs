using Bakeshop.EF.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bakeshop.ViewModels
{
    public class ReceiptViewModel : ObservableObject, IDisposable
    {
        private ObservableCollection<BakeryProduct> _bakeryProducts;

        public Grid PrintView { get; set; }

        public ReceiptViewModel()
        {
            PrintCommand = new RelayCommand(Print);
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
        }

        public ObservableCollection<BakeryProduct> BakeryProducts
        {
            get { return _bakeryProducts; }
            set { Set(ref _bakeryProducts, value); }
        }

        public ICommand PrintCommand { get; set; }

        public ICommand GetToPreviousWindowCommand { get; set; }

        public void LoadOrderedProducts(ObservableCollection<BakeryProduct> bakeryProducts)
        {
            BakeryProducts = bakeryProducts;
            RaisePropertyChanged("BakeryProducts");
        }

        public void GetToPreviousWindow()
        {
            CloseAction();
        }

        public void Print()
        {
            var printdialog = new PrintDialog();
            if (printdialog.ShowDialog() == true)
            {
                printdialog.PrintVisual(PrintView, "Receipt");
            }
        }

        public Action CloseAction { get; set; }

        public void Dispose()
        {
            Dispose();
        }
    }
}
