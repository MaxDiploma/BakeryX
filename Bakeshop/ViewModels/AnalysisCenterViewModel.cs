using GalaSoft.MvvmLight;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Bakeshop.EF;
using Bakeshop.Views;
using GalaSoft.MvvmLight.CommandWpf;
using OxyPlot.Axes;

namespace Bakeshop.ViewModels
{
    public class AnalysisCenterViewModel : ObservableObject, IDisposable
    {
        private string _title;
        private IList<DataPoint> _dataPoints;
        private PlotModel _model;
        private readonly BakeshopContext _context;
        public DateTime _from;
        public DateTime _to;

        public AnalysisCenterViewModel()
        {
            _context = new BakeshopContext();
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
        }

        public IList<DataPoint> DataPoints
        {
            get { return _dataPoints; }
            set { Set(ref _dataPoints, value); }
        }

        public bool IsNeedToOpen { get; set; }

        public PlotModel Model
        {
            get { return _model; }
            set { Set(ref _model, value); }
        }

        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        public Action CloseAction { get; set; }

        public ICommand GetToPreviousWindowCommand { get; private set; }

        public async void GetToPreviousWindow()
        {
            var menu = new MenuView();
            menu.Show();
            CloseAction();
        }

        public bool LoadGraphData()
        {
            var plot = new PlotModel();
            plot.Title = "Sales analysis";
            plot.TitleFont = "Helvetica";
            plot.TitleFontSize = 20;

            var sales = _context.Sales
                .Where(s => s.TransactionDate >= _from && s.TransactionDate < _to)
                .ToList();

            if (sales.Count != 0)
            {

                var grouppedSales = sales.GroupBy(s => s.Name);

                var minDate = DateTimeAxis.ToDouble(sales.Min(s => s.TransactionDate));
                var maxDate = DateTimeAxis.ToDouble(sales.Max(s => s.TransactionDate));

                var minQuantity = sales.Min(s => s.Quantity) - 50;
                var maxQuantity = sales.Max(s => s.Quantity) + 50;

                plot.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = minQuantity, Maximum = maxQuantity, IsZoomEnabled = false, IsPanEnabled = false });
                plot.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, Minimum = minDate, Maximum = maxDate, StringFormat = "M/d", IsZoomEnabled = false, IsPanEnabled = false });

                foreach (var group in grouppedSales)
                {
                    var series = new LineSeries();

                    foreach (var sale in group)
                    {
                        series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(sale.TransactionDate), sale.Quantity));
                        series.Title = group.Key;
                    }

                    plot.Series.Add(series);
                }

                Model = plot;

                return true;
            }

            MessageBox.Show("There are no sales for enter dates", "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);

            return false;
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
