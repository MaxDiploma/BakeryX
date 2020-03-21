using Bakeshop.CommandHandler;
using Bakeshop.DomainModels;
using Bakeshop.EF;
using Bakeshop.Extensions;
using Bakeshop.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bakeshop.ViewModels
{
    public class RecipesViewModel : ObservableObject, IDisposable
    {
        private BakeshopContext _context;
        private ObservableCollection<RecipeDomain> _formula;
        private ICommand _searchCommand;
        private int _page;
        private int _pagePayload = 10;
        private bool _isNextPageButtonEnabled;
        private bool _isPreviousPageButtonEnabled;

        public RecipesViewModel()
        {
            _context = new BakeshopContext();
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            NextPageCommand = new RelayCommand(NextPage);
            PreviousPageCommand = new RelayCommand(PreviousPage);
            IsNextPageButtonEnabled = false;
            IsPreviousPageButtonEnabled = true;
            LoadFormulas();
        }

        public ObservableCollection<RecipeDomain> Formulas
        {
            get { return _formula; }
            set { Set(ref _formula, value); }
        }

        public bool IsNextPageButtonEnabled
        {
            get { return _isNextPageButtonEnabled; }
            set { Set(ref _isNextPageButtonEnabled, value); }
        }

        public bool IsPreviousPageButtonEnabled
        {
            get { return _isPreviousPageButtonEnabled; }
            set { Set(ref _isPreviousPageButtonEnabled, value); }
        }

        public ICommand GetToPreviousWindowCommand { get; set; }

        public ICommand PreviousPageCommand { get; set; }

        public ICommand NextPageCommand { get; set; }

        public Action CloseAction { get; set; }


        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new BaseCommandHandler(param => SearchHandler(param), true));
            }
        }

        public async void LoadFormulas()
        {
            Formulas = new ObservableCollection<RecipeDomain>();

            var formulas = await _context.Formulas
                .Include(f => f.FormulaIngredients.Select(fi => fi.Ingredient))
                .Include(f => f.FormulaIngredients.Select(fi => fi.Formula))
                .OrderBy(f => f.Name)
                .Skip(_page * _pagePayload)
                .Take(_pagePayload)
                .ToListAsync();

            foreach (var formula in formulas)
            {
                Formulas.Add(formula.ToDomain());
            }

            IsPreviousPageButtonEnabled = false;
        }

        private async void SearchHandler(object param)
        {
            Formulas.Clear();

            var textBox = param as TextBox;
            var searchQuery = textBox.Text;

            var formulas = await _context.Formulas
                .Where(p => p.Name.Contains(searchQuery))
                .ToListAsync();

            foreach (var formula in formulas)
            {
                Formulas.Add(formula.ToDomain());
            }

            RaisePropertyChanged("Formulas");
        }

        public void GetToPreviousWindow()
        {
            var menu = new MenuView();
            menu.Show();
            CloseAction();
        }

        public async void NextPage()
        {
            Formulas.Clear();

            _page++;


            var formulas = await _context.Formulas
                            .OrderBy(x => x.Name)
                            .Skip(_pagePayload * _page)
                            .Take(_pagePayload)
                            .ToListAsync();

            foreach (var formula in formulas)
            {
                Formulas.Add(formula.ToDomain());
            }

            RaisePropertyChanged("Formulas");

            IsPreviousPageButtonEnabled = true;

            if (formulas.Count >= 10)
            {
                IsNextPageButtonEnabled = true;
            }
            else
            {
                IsNextPageButtonEnabled = false;
            }
        }

        public async void PreviousPage()
        {
            Formulas.Clear();

            _page--;

            var formulas = await _context.Formulas
                            .OrderBy(x => x.Name)
                            .Skip(_pagePayload * _page)
                            .Take(_pagePayload)
                            .ToListAsync();

            foreach (var formula in formulas)
            {
                Formulas.Add(formula.ToDomain());
            }

            RaisePropertyChanged("Formulas");

            if (_page == 0)
            {
                IsPreviousPageButtonEnabled = false;
            }

            if (formulas.Count >= 10)
            {
                IsNextPageButtonEnabled = true;
            }
        }

        public void Dispose()
        {
            Dispose();
        }
    }
}
