using Bakeshop.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows.Input;

namespace Bakeshop.ViewModels
{
    public class MenuViewModel : ObservableObject, IDisposable
    {

        public MenuViewModel()
        {
            OpenEmployeesCommand = new RelayCommand(MoveToEmployees);
            OpenWarehouseCommand = new RelayCommand(MoveToWarehouse);
            OpenSuppliersCommand = new RelayCommand(MoveToSuppliers);
            OpenRecipesCommand = new RelayCommand(MoveToRecipes);
            OpenWorkstationCommand = new RelayCommand(MoveToWorkstation);
            OpenAnalysisCenter = new RelayCommand(MoveToAnalysisCenter);
            OpenSalesCommand = new RelayCommand(MoveToSales);
        }

        public ICommand OpenEmployeesCommand { get; private set; }

        public ICommand OpenWarehouseCommand { get; private set; }

        public ICommand OpenSuppliersCommand { get; private set; }

        public ICommand OpenRecipesCommand { get; private set; }

        public ICommand OpenWorkstationCommand { get; private set; }

        public ICommand OpenAnalysisCenter { get; private set; }

        public ICommand OpenSalesCommand { get; private set; }

        public Action CloseAction { get; set; }

        private void MoveToEmployees()
        {
            var employees = new EmployeeView();
            employees.Show();
            CloseAction();
        }

        private void MoveToWarehouse()
        {
            var warehouse = new WarehouseView();
            warehouse.Show();
            CloseAction();
        }

        private void MoveToSuppliers()
        {
            var suppliers = new SuppliersView();
            suppliers.Show();
            CloseAction();
        }

        private void MoveToRecipes()
        {
            var recipes = new RecipesView();
            recipes.Show();
            CloseAction();
        }
        private void MoveToWorkstation()
        {
            var workstation = new WorkstationView();
            workstation.Show();
            CloseAction();
        }

        private void MoveToAnalysisCenter()
        {
            var analysisCenterDatesPicker = new AnalysisCenterDatesPicker();
            analysisCenterDatesPicker.Show();
            CloseAction();
        }

        private void MoveToSales()
        {
            var sales = new SalesView();
            sales.Show();
            CloseAction();
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
