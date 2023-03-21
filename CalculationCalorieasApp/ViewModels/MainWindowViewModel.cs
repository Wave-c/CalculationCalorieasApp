﻿using CalculationCalorieasApp.Helpers;
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
using System.Windows.Media.Imaging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace CalculationCalorieasApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public class Product { }
        private User _currentUser;
        private int _calorieAllowance;
        private int _sumCaloriesPerDay;
        private IEnumerable<Product> _products;
        private IEnumerable<Product> _breakfastProducts;
        private IEnumerable<Product> _dinnerProducts;
        private IEnumerable<Product> _supperProducts;
        private Product _selectedProduct;

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
        public Eating Eating { get; set; }
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

        private void AddProductCommand_Execute()
        {
            switch(Eating)
            {
                case Eating.BREAKFAST:
                    BreakfastProducts.Append(SelectedProduct);
                    break;
                case Eating.DINNER:
                    DinnerProducts.Append(SelectedProduct);
                    break;
                case Eating.SUPPER:
                    SupperProducts.Append(SelectedProduct);
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


        //public string CheckRadBtnProducts()
        //{
        //    string productTime;
        //    if (RadioButtonBreakfast.IsChecked == true)
        //    {
        //        productTime = "Breakfast";
        //        return productTime;
        //    }
        //    else if (RadioButtonDinner.IsChecked == true)
        //    {
        //        productTime = "Dinner";
        //        return productTime;
        //    }
        //    else if (RadioButtonSupper.IsChecked == true)
        //    {
        //        productTime = "Supper";
        //        return productTime;
        //    }
        //    else
        //    {
        //        productTime = "None";
        //        return productTime;
        //    }

        //}
        public void CountCalories()
        {
            decimal calories = 0;
            
            List<Products> breakfast_food = new List<Products>();
            List<Products> dinner_food = new List<Products>();
            List<Products> supper_food = new List<Products>();
            foreach (var item in _breakfeastLbx.Items)
                breakfast_food.Add((Products)item);
            foreach (var item in _dinnerLbx.Items)
                dinner_food.Add((Products)item);
            foreach (var item in _supperLbx.Items)
                supper_food.Add((Products)item);

            foreach (var item in breakfast_food)
            {
                calories += item.Calories;
                
            }
            foreach (var item in dinner_food)
            {
                calories += item.Calories;
               
            }
            foreach (var item in supper_food)
            {
                calories += item.Calories;
               
            }

            SumCalloriesTxtBl.Text = calories.ToString();
            
        }
    }
}
