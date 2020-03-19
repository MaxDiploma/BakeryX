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

        public void MoveToAnalysisCenter()
        {
            var analysisCenter = new AnalysisCenterView();
            analysisCenter.DataContext = new AnalysisCenterViewModel(From, To)
            {
                CloseAction = ((AnalysisCenterViewModel)analysisCenter.DataContext).CloseAction
            };

            analysisCenter.Show();

            CloseAction();
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
