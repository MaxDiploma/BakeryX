using System;
using System.Windows.Input;
using Bakeshop.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Bakeshop.ViewModels
{
    public class AnalysisCenterDatesPickerModel : ObservableObject, IDisposable
    {
        public DateTime _from;
        public DateTime _to;

        public AnalysisCenterDatesPickerModel()
        {
            From = DateTime.UtcNow;
            To = DateTime.UtcNow;
            OpenAnalasysCenterCommand = new RelayCommand(MoveToAnalysisCenter);
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
        }

        public DateTime From
        {
            get { return _from; }
            set { Set(ref _from, value); }
        }

        public DateTime To
        {
            get { return _to; }
            set { Set(ref _to, value); }
        }

        public Action CloseAction { get; set; }

        public ICommand OpenAnalasysCenterCommand { get; set; }

        public ICommand GetToPreviousWindowCommand { get; set; }

        public void MoveToAnalysisCenter()
        {
            var analysisCenter = new AnalysisCenterView();
            var dataContext = analysisCenter.DataContext as AnalysisCenterViewModel;

            dataContext._from = From;
            dataContext._to = To;
            var isNeedToOpen = dataContext.LoadGraphData();

            if (isNeedToOpen)
            {
                analysisCenter.Show();
                CloseAction();
            }
        }

        public void GetToPreviousWindow()
        {
            var menu = new MenuView();
            menu.Show();
            CloseAction();
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
