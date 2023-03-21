using CalculationCalorieasApp.Helpers;
using CalculationCalorieasApp.Medels.Entitys;
using CalculationCalorieasApp.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace CalculationCalorieasApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private BitmapImage _image;
        private User _currentUser;

        public MainWindowViewModel(User currentUser)
        {
            _currentUser = currentUser;
            HasUserAdminOptions = currentUser.Status == StatusUser.ADMIN ? true : false;
        }

        public bool HasUserAdminOptions { get; set; }
        public BitmapImage Image
        {
            get => _image;
            set
            {
                _image = value;
                RaisePropertyChanged();
            }
        }

        private DelegateCommand _changeImageCommand;
        public DelegateCommand ChangeImageCommand => _changeImageCommand ??= new DelegateCommand(ChangeImageCommand_Execute);

        private async void ChangeImageCommand_Execute()
        {
            Image = await BitmapHelper.SetUserImageAsync(_currentUser);
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
