using Bakeshop.CommandHandler;
using Bakeshop.Common.Enums;
using Bakeshop.DomainModels;
using Bakeshop.EF;
using Bakeshop.Extensions;
using Bakeshop.StaticResources;
using Bakeshop.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bakeshop.ViewModels
{
    public class RecipesViewModel : ObservableObject, IDisposable
    {
        private BakeshopContext _context;
        private ObservableCollection<RecipeDomain> _formula;
        private RecipeDomain selectedFormula;
        private ICommand _searchCommand;
        private int _page;
        private int _pagePayload = 10;
        private bool _isNextPageButtonEnabled;
        private bool _isPreviousPageButtonEnabled;
        private Visibility _isUserAdminOrOwnerVisability;

        public RecipesViewModel()
        {
            _context = new BakeshopContext();
            GetToPreviousWindowCommand = new RelayCommand(GetToPreviousWindow);
            NextPageCommand = new RelayCommand(NextPage);
            PreviousPageCommand = new RelayCommand(PreviousPage);
            AddNewRecipeCommand = new RelayCommand(AddNewRecipe);
            EditRecipeCommand = new RelayCommand(EditRecipe);
            DeleteRecipeCommand = new RelayCommand(DeleteRecipe);
            IsNextPageButtonEnabled = false;
            IsPreviousPageButtonEnabled = true;
            LoadFormulas();

            var currentUser = CurrentUserManagment.GetCurrentUser();
            var IsAdminOrOwner = currentUser.Position == Positions.Owner || currentUser.Position == Positions.Manager ? true : false;
            IsAdminOrOwnerVisability = IsAdminOrOwner ? Visibility.Visible : Visibility.Hidden;
        }

        public ObservableCollection<RecipeDomain> Formulas
        {
            get { return _formula; }
            set { Set(ref _formula, value); }
        }

        public Visibility IsAdminOrOwnerVisability
        {
            get { return _isUserAdminOrOwnerVisability; }
            set { Set(ref _isUserAdminOrOwnerVisability, value); }
        }

        public RecipeDomain SelectedFormula
        {
            get { return selectedFormula; }
            set { Set(ref selectedFormula, value); }
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

        public ICommand AddNewRecipeCommand { get; set; }

        public ICommand EditRecipeCommand { get; set; }

        public ICommand DeleteRecipeCommand { get; set; }

        public Action CloseAction { get; set; }

        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new BaseCommandHandler(param => SearchHandler(param), true));
            }
        }

        public void AddNewRecipe()
        {
            var newRecipe = new NewRecipeView();
            newRecipe.DataContext = new NewRecipeViewModel()
            {
                CloseAction = ((NewRecipeViewModel)newRecipe.DataContext).CloseAction
            };
            newRecipe.ShowDialog();
            _context = new BakeshopContext();
            LoadFormulas();
        }

        public void EditRecipe()
        {
            if (SelectedFormula == null)
            {
                return;
            }

            var editRecipe = new EditRecipeView();
            editRecipe.DataContext = new EditRecipeViewModel(SelectedFormula.Id)
            {
                CloseAction = ((EditRecipeViewModel)editRecipe.DataContext).CloseAction
            };
            editRecipe.ShowDialog();
            _context = new BakeshopContext();
            LoadFormulas();
        }

        public void DeleteRecipe()
        {
            if (SelectedFormula == null)
            {
                return;
            }

            var formula = _context.Formulas.FirstOrDefault(f => f.Id == SelectedFormula.Id);
            _context.Formulas.Remove(formula);

            _context.SaveChanges();
            LoadFormulas();
        }

        public void LoadFormulas()
        {
            Formulas = new ObservableCollection<RecipeDomain>();

            var formulas = _context.Formulas
                .Include(f => f.FormulaIngredients.Select(fi => fi.Ingredient))
                .Include(f => f.FormulaIngredients.Select(fi => fi.Formula))
                .OrderBy(f => f.Name)
                .Skip(_page * _pagePayload)
                .Take(_pagePayload)
                .ToList();

            var formulasCount = _context.Formulas.Count();

            foreach (var formula in formulas)
            {
                Formulas.Add(formula.ToDomain());
            }

            if (formulasCount > 10)
            {
                IsNextPageButtonEnabled = true;
            }
            else
            {
                IsPreviousPageButtonEnabled = false;
            }


            RaisePropertyChanged("Formulas");
            RaisePropertyChanged("IsPreviousPageButtonEnabled");
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
