using CalculationCalorieasApp.Medels;
using CalculationCalorieasApp.Medels.Entitys;
using CalculationCalorieasApp.Medels.Enums;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using CalculationCalorieasApp.Views;


namespace CalculationCalorieasApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private User _currentUser;
        private int _calorieAllowance;
        private int _sumCaloriesPerDay;
        private IEnumerable<Product> _products = new List<Product>();
        private List<Product> _breakfastProducts = new List<Product>();
        private List<Product> _dinnerProducts = new List<Product>();
        private List<Product> _supperProducts = new List<Product>();
        private Product _selectedProduct;
        private Eating _eating;
        private int _breakfastCcal;
        private int _dinnerCcal;
        private int _supperCcal;

        public MainWindowViewModel(User currentUser)
        {
            _currentUser = currentUser;
            HasUserAdminOptions = currentUser.Status == StatusUser.ADMIN ? true : false;
            Eating = Eating.NA;
            CalorieAllowance = currentUser.CalPerDay;
            SumCaloriesPerDay = 0;
        }

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                RaisePropertyChanged();
                AddProductCommand.RaiseCanExecuteChanged();
            }
        }
        public List<Product> SupperProducts
        {
            get => _supperProducts;
            set
            {
                _supperProducts = value;
                RaisePropertyChanged();
                RemoveProductCommand.RaiseCanExecuteChanged();
            }
        }
        public List<Product> DinnerProducts
        {
            get => _dinnerProducts;
            set
            {
                _dinnerProducts = value;
                RaisePropertyChanged();
                RemoveProductCommand.RaiseCanExecuteChanged();
            }
        }
        public List<Product> BreakfastProducts
        {
            get => _breakfastProducts;
            set
            {
                _breakfastProducts = value;
                RaisePropertyChanged();
                RemoveProductCommand.RaiseCanExecuteChanged();
            }
        }
        public IEnumerable<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                RaisePropertyChanged();
            }
        }
        public Eating Eating
        {
            get => _eating;
            set
            {
                _eating = value;
                AddProductCommand.RaiseCanExecuteChanged();
            }
        }
        public int SumCaloriesPerDay
        {
            get => _sumCaloriesPerDay;
            set
            {
                _sumCaloriesPerDay = value;
                RaisePropertyChanged();
            }
        }
        public int CalorieAllowance
        {
            get => _calorieAllowance;
            set
            {
                _calorieAllowance = value;
                RaisePropertyChanged();
            }
        }
        public int BreakfastCcal
        {
            get => _breakfastCcal;
            set
            {
                _breakfastCcal = value;
                RaisePropertyChanged();
            }
        }
        public int DinnerCcal
        {
            get => _dinnerCcal;
            set
            {
                _dinnerCcal = value;
                RaisePropertyChanged();
            }
        }
        public int SupperCcal
        {
            get => _supperCcal;
            set
            {
                _supperCcal = value;
                RaisePropertyChanged();
            }
        }

        public bool HasUserAdminOptions { get; set; }

        private DelegateCommand _addProductCommand;
        public DelegateCommand AddProductCommand => _addProductCommand ??= new DelegateCommand(AddProductCommand_Execute, AddProductCommand_CanExecute);

        private DelegateCommand _removeProductCommand;
        public DelegateCommand RemoveProductCommand => _removeProductCommand ??= new DelegateCommand(RemoveProductCommand_Execute, RemoveProductCommand_CanExecute);

        private void RemoveProductCommand_Execute()
        {
            var breakfastProduct = BreakfastProducts.ToList();
            var dinnerProduct = DinnerProducts.ToList();
            var supperProduct = SupperProducts.ToList();

            if (breakfastProduct.Contains(SelectedProduct))
            {
                breakfastProduct.Remove(SelectedProduct);
                SumCaloriesPerDay -= SelectedProduct.Calories;
                BreakfastCcal -= SelectedProduct.Calories;
                BreakfastProducts = breakfastProduct;
            }
            else if (dinnerProduct.Contains(SelectedProduct))
            {
                dinnerProduct.Remove(SelectedProduct);
                SumCaloriesPerDay -= SelectedProduct.Calories;
                DinnerCcal -= SelectedProduct.Calories;
                DinnerProducts = dinnerProduct;
            }
            else if (supperProduct.Contains(SelectedProduct))
            {
                supperProduct.Remove(SelectedProduct);
                SumCaloriesPerDay -= SelectedProduct.Calories;
                SupperCcal -= SelectedProduct.Calories;
                SupperProducts = supperProduct;
            }
            SelectedProduct = null;
        }
        private bool RemoveProductCommand_CanExecute()
        {
            return SupperProducts.Count != 0 || DinnerProducts.Count != 0 || BreakfastProducts.Count != 0;
        }

        private void AddProductCommand_Execute()
        {
            switch(Eating)
            {
                case Eating.BREAKFAST:
                    BreakfastCcal += SelectedProduct.Calories;
                    BreakfastProducts = BreakfastProducts.Append(SelectedProduct).ToList();
                    break;
                case Eating.DINNER:
                    DinnerCcal += SelectedProduct.Calories;
                    DinnerProducts = DinnerProducts.Append(SelectedProduct).ToList();
                    break;
                case Eating.SUPPER:
                    SupperCcal += SelectedProduct.Calories;
                    SupperProducts = SupperProducts.Append(SelectedProduct).ToList();
                    break;
                case Eating.NA:
                    MessageBox.Show("Выберите приём еды", "Error", MessageBoxButton.OK);
                    break;
            }
            SumCaloriesPerDay += SelectedProduct.Calories;
        }
        private bool AddProductCommand_CanExecute()
        {
            return Eating != Eating.NA && SelectedProduct != null;
        }
        private DelegateCommand _openPersonalAccountCommand;
        public DelegateCommand OpenPersonalAccountCommand => _openPersonalAccountCommand ??= new DelegateCommand(OpenPersonalAccountCommand_Execute);

        private void OpenPersonalAccountCommand_Execute()
        {
            var personalAccountWindow = new PersonalAccountWindow(_currentUser);
            personalAccountWindow.Show();
        }

        public async Task UpdateProducts()
        {
            using (var dbContext = new AppDBContext())
            {
                Products = await dbContext.Products.ToListAsync();
            }
        }
    }
}
