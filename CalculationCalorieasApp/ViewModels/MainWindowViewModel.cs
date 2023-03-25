using CalculationCalorieasApp.Helpers;
using CalculationCalorieasApp.Medels;
using CalculationCalorieasApp.Medels.Entitys;
using CalculationCalorieasApp.Medels.Enums;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;



namespace CalculationCalorieasApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private User _currentUser;
        private int _breakfastCcal;
        private int _dinnerCcal;
        private int _supperCcal;
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

        public int BreakfastCcal
        {
            get=> _breakfastCcal;
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

        private DelegateCommand _countCcalCommad;
        public DelegateCommand CountCcalProductCommand => _countCcalCommad ??= new DelegateCommand(CountCcalProductCommand_Execute, CountCcalProductCommand_CanExecute);
        private void RemoveProductCommand_Execute()
        {
            
            var breakfastProduct=BreakfastProducts.ToList();
            var dinnerProduct=DinnerProducts.ToList();
            var supperProduct=SupperProducts.ToList();

            if (breakfastProduct.Contains(SelectedProduct))
            {
                breakfastProduct.Remove(SelectedProduct);
                BreakfastProducts = breakfastProduct;
                SelectedProduct = null;
            }
            else if(dinnerProduct.Contains(SelectedProduct))
            { dinnerProduct.Remove(SelectedProduct);
                DinnerProducts = dinnerProduct;
                SelectedProduct = null;
            }
            else if(supperProduct.Contains(SelectedProduct))
            { 
                supperProduct.Remove(SelectedProduct);
                SupperProducts = supperProduct;
                SelectedProduct = null;
            }

            CountCcalProductCommand_Execute();
        }
        private bool RemoveProductCommand_CanExecute()
        {
            return SupperProducts.Count() != 0 || DinnerProducts.Count() != 0 || BreakfastProducts.Count() != 0;
        }

        private void AddProductCommand_Execute()
        {
           
            switch (Eating)
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

        private void CountCcalProductCommand_Execute()
        {
            BreakfastCcal = 0;
            DinnerCcal = 0;
            SupperCcal = 0;
            SumCaloriesPerDay = 0;
            var breakfastProduct = BreakfastProducts.ToList();
            var dinnerProduct = DinnerProducts.ToList();
            var supperProduct = SupperProducts.ToList();
            foreach( var product in breakfastProduct)
            {
                BreakfastCcal += product.Calories;
            }
            foreach( var product in dinnerProduct)
            {
                DinnerCcal+= product.Calories;
            }
            foreach ( var product in supperProduct)
            {
                SupperCcal+= product.Calories;
            }

            SumCaloriesPerDay = BreakfastCcal + DinnerCcal + SupperCcal;

        }
        private bool CountCcalProductCommand_CanExecute()
        {
            return SelectedProduct != null;
        }

        //public async Task UpdateProducts()
        //{
        //    using (var dbContext = new AppDBContext())
        //    {
        //        Products = await dbContext.Products.ToListAsync();
        //    }
        //}


    }
}
