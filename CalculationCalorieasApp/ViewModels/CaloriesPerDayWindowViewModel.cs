using CalculationCalorieasApp.Medels.Entitys;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using System.Windows;
using Prism.Mvvm;
using CalculationCalorieasApp.Helpers;
using CalculationCalorieasApp.Medels;
using CalculationCalorieasApp.Medels.Enums;
using CalculationCalorieasApp.Views;

namespace CalculationCalorieasApp.ViewModels
{
    public class CaloriesPerDayWindowViewModel:BindableBase
    {
        private User _user;
        private CaloriesPerDayWindow _window;
        public CaloriesPerDayWindowViewModel(User user, CaloriesPerDayWindow window)
        {
            _window = window;
            _user = user;
        }
        private Goal _selectedGoal;
        public Goal SelectedGoal
        {
            get => _selectedGoal;
            set
            {
                _selectedGoal = value;
                RaisePropertyChanged();
                ResultCommand.RaiseCanExecuteChanged();
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        private Gender _selectedGender;
        public Gender SelectedGender
        {
            get => _selectedGender;
            set
            {
                _selectedGender = value;
                RaisePropertyChanged();
                ResultCommand.RaiseCanExecuteChanged();
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }
        private Activ _selectedActiv;
        public Activ SelectedActiv
        {
            get => _selectedActiv;
            set
            {
                _selectedActiv = value;
                RaisePropertyChanged();
                ResultCommand.RaiseCanExecuteChanged();
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }
        private string _weight;
        public string Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                RaisePropertyChanged();
                ResultCommand.RaiseCanExecuteChanged();
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }
        private string _height;
        public string Height
        {
            get => _height;
            set
            {
                _height = value;
                RaisePropertyChanged();
                ResultCommand.RaiseCanExecuteChanged();
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }
        private string _age;
        public string Age
        {
            get => _age;
            set
            {
                _age = value;
                RaisePropertyChanged();
                ResultCommand.RaiseCanExecuteChanged();
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }
        private string _result;
        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                RaisePropertyChanged();
                ResultCommand.RaiseCanExecuteChanged();
                RegisterCommand.RaiseCanExecuteChanged();
            }
        }

        private DelegateCommand _resultCommand;
        public DelegateCommand ResultCommand =>
                    _resultCommand ??= new DelegateCommand(ResultCommand_Execute, ResultCommand_CanExecute);

        private DelegateCommand _closeWindowCommand;
        public DelegateCommand CloseWindowCommand => _closeWindowCommand ??= new DelegateCommand(CloseWindowCommand_Execute);

        private DelegateCommand _registerCommand;
        public DelegateCommand RegisterCommand => _registerCommand ??= new DelegateCommand(RegisterCommand_Execute, SaveCommand_CanExecute);

        private async void RegisterCommand_Execute()
        {
            using(var dbContext = new AppDBContext())
            {
                var addedUser = await dbContext.Users.Where(x => x.Id == _user.Id).FirstAsync();
                addedUser.Goal = SelectedGoal;
                addedUser.Gender = SelectedGender;
                addedUser.Activ = SelectedActiv;
                addedUser.Weight = int.Parse(Weight);
                addedUser.Height = int.Parse(Height);
                addedUser.Age = int.Parse(Age);
                addedUser.CalPerDay = int.Parse(Result);
                await dbContext.SaveChangesAsync();
            }
            _window.DialogResult = true;
            _window.Close();
        }

        private async void CloseWindowCommand_Execute()
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.Remove(_user);
                await dbContext.SaveChangesAsync();
            }
            _window.DialogResult = false;
            _window.Close();
        }

        public void ResultCommand_Execute()
        {
            int i = 0;
            if (SelectedGender == Gender.Man)
                i = 5;
            else i = -161;
            double result = ((9.99 * Convert.ToInt32(Weight)) + (6.25 * Convert.ToInt32(Height)) - (4.92 * Convert.ToInt32(Age)) + (i))* SwitchEnumHelper.EnumConverter(SelectedActiv);
            switch (SelectedGoal)
            {
                case Goal.Increase:
                    result += (result /100*20);
                    break;
                case Goal.Decrease:
                    result -= (result / 100 * 20);
                    break;
                case Goal.Save:
                    break;

            }
          
            Result= ((int)result).ToString();
        }
        public bool ResultCommand_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Weight) &&
                !string.IsNullOrWhiteSpace(Height) &&
            !string.IsNullOrWhiteSpace(Age);
        }

        public bool SaveCommand_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Weight) &&
                !string.IsNullOrWhiteSpace(Height) &&
            !string.IsNullOrWhiteSpace(Age);
        }

    }
}
