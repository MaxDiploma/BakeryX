using BakeryX.Common.MonitorManagment;
using BakeryX.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using System.Windows.Input;

namespace BakeryX.ViewModels
{
    public class MenuViewModel : ObservableObject, IDisposable
    {
        private int _displayWidth;
        private int _displayHeight;
        private Thickness _menuButtomTopMargin;

        public MenuViewModel()
        {
            DisplayWidth = MonitorProperties.DisplayWidth;
            DisplayHeight = MonitorProperties.DisplayHeight;
            MenuButtomTopMargin = new Thickness(0, DisplayHeight / 9, 0, 0);
            OpenEmployeeInformationCommand = new RelayCommand(MoveToEmployeeInformation);
        }

        public ICommand OpenEmployeeInformationCommand { get; private set; }

        public int DisplayWidth
        {
            get { return _displayWidth; }
            set { Set(ref _displayWidth, value); }
        }

        public int DisplayHeight
        {
            get { return _displayHeight; }
            set { Set(ref _displayHeight, value); }
        }

        public Thickness MenuButtomTopMargin
        {
            get { return _menuButtomTopMargin; }
            set { Set(ref _menuButtomTopMargin, value); }
        }

        public Action CloseAction { get; set; }

        private void MoveToEmployeeInformation()
        {
            var employeeInformation = new EmployeeInformation();
            employeeInformation.Show();
            CloseAction();
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
