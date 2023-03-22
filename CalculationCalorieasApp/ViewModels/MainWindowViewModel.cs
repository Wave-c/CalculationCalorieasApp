using CalculationCalorieasApp.Medels;
using CalculationCalorieasApp.Medels.Entitys;
using CalculationCalorieasApp.Medels.Enums;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace CalculationCalorieasApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private User _currentUser;
        private int _calorieAllowance;
        private int _sumCaloriesPerDay;
        private IEnumerable<Product> _products = new List<Product>();
        private IEnumerable<Product> _breakfastProducts = new List<Product>();
        private IEnumerable<Product> _dinnerProducts = new List<Product>();
        private IEnumerable<Product> _supperProducts = new List<Product>();
        private Product _selectedProduct;
        private Eating _eating;

        public MainWindowViewModel(User currentUser)
        {
            _currentUser = currentUser;
            HasUserAdminOptions = currentUser.Status == StatusUser.ADMIN ? true : false;
            Eating = Eating.NA;
            
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
        public IEnumerable<Product> SupperProducts
        {
            get => _supperProducts;
            set
            {
                _supperProducts = value;
                RaisePropertyChanged();
            }
        }
        public IEnumerable<Product> DinnerProducts
        {
            get => _dinnerProducts;
            set
            {
                _dinnerProducts = value;
                RaisePropertyChanged();
            }
        }
        public IEnumerable<Product> BreakfastProducts
        {
            get => _breakfastProducts;
            set
            {
                _breakfastProducts = value;
                RaisePropertyChanged();
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

        public bool HasUserAdminOptions { get; set; }

        private DelegateCommand _addProductCommand;
        public DelegateCommand AddProductCommand => _addProductCommand ??= new DelegateCommand(AddProductCommand_Execute, AddProductCommand_CanExecute);

        private DelegateCommand _removeProductCommand;
        public DelegateCommand RemoveProductCommand => _removeProductCommand ??= new DelegateCommand(RemoveProductCommand_Execute, RemoveProductCommand_CanExecute);

        private void RemoveProductCommand_Execute()
        {

        }
        private bool RemoveProductCommand_CanExecute()
        {
            return SupperProducts.Count() != 0 || DinnerProducts.Count() != 0 || BreakfastProducts.Count() != 0;
        }

        private void AddProductCommand_Execute()
        {
            switch(Eating)
            {
                case Eating.BREAKFAST:
                    BreakfastProducts = BreakfastProducts.Append(SelectedProduct);
                    break;
                case Eating.DINNER:
                    DinnerProducts = DinnerProducts.Append(SelectedProduct);
                    break;
                case Eating.SUPPER:
                    SupperProducts = SupperProducts.Append(SelectedProduct);
                    break;
                case Eating.NA:
                    MessageBox.Show("Выберите приём еды", "Error", MessageBoxButton.OK);
                    break;
            }
        }
        private bool AddProductCommand_CanExecute()
        {
            return Eating != Eating.NA && SelectedProduct != null;
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
