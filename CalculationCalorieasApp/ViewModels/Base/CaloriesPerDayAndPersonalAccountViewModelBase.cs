using CalculationCalorieasApp.Helpers;
using CalculationCalorieasApp.Medels;
using CalculationCalorieasApp.Medels.Entitys;
using CalculationCalorieasApp.Medels.Enums;
using CalculationCalorieasApp.Views;
using CalculationCalorieasApp.Views.Interfaces;
using Messager.Helpers;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CalculationCalorieasApp.ViewModels.Base
{
    public abstract class CaloriesPerDayAndPersonalAccountViewModelBase : BindableBase
    {
        private User _user;
        protected ILoginOrRegisterWindow _window;
        public CaloriesPerDayAndPersonalAccountViewModelBase(ILoginOrRegisterWindow window, User user)
        {
            _user = user;
            _window = window;
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
        protected DelegateCommand _resultCommand;
        public DelegateCommand ResultCommand => _resultCommand ??= new DelegateCommand(ResultCommand_Execute, ResultCommand_CanExecute);

        protected abstract bool ResultCommand_CanExecute();
        protected virtual void ResultCommand_Execute()
        {
            int i = 0;
            if (SelectedGender == Gender.Man)
                i = 5;
            else i = -161;
            double result = ((9.99 * Convert.ToInt32(Weight)) + (6.25 * Convert.ToInt32(Height)) - (4.92 * Convert.ToInt32(Age)) + (i)) * SwitchEnumHelper.EnumConverter(SelectedActiv);
            switch (SelectedGoal)
            {
                case Goal.Increase:
                    result += (result / 100 * 20);
                    break;
                case Goal.Decrease:
                    result -= (result / 100 * 20);
                    break;
                case Goal.Save:
                    break;

            }
            Result = ((int)result).ToString();
        }

        protected DelegateCommand _closeWindowCommand;
        public DelegateCommand CloseWindowCommand => _closeWindowCommand ??= new DelegateCommand(CloseWindowCommand_Execute);
        protected virtual async void CloseWindowCommand_Execute()
        {

            using (var dbContext = new AppDBContext())
            {
                dbContext.Remove(_user);
                await dbContext.SaveChangesAsync();
            }
            ((Window)_window).DialogResult = false;
            ((Window)_window).Close();
        }

        protected DelegateCommand _registerCommand;
        public DelegateCommand RegisterCommand => _registerCommand ??= new DelegateCommand(RegisterCommand_Execute, RegisterCommand_CanExecute);

        protected abstract bool RegisterCommand_CanExecute();
        protected virtual async void RegisterCommand_Execute()
        {
            using (var dbContext = new AppDBContext())
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
            ((Window)_window).DialogResult = true;
            ((Window)_window).Close();
        }
        public void CloseWindow()
        {
            ((Window)_window).Close();
        }
        protected DelegateCommand _saveLoginAndPasswordCommand;
        public DelegateCommand SaveLoginAndPasswordCommand => _saveLoginAndPasswordCommand ??= new DelegateCommand(SaveLoginAndPasswordCommand_Execute, SaveLoginAndPasswordCommand_CanExecute);

        protected abstract bool SaveLoginAndPasswordCommand_CanExecute();
        protected virtual void SaveLoginAndPasswordCommand_Execute()
        {
            ((Window)_window).Close();
        }

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand ??= new DelegateCommand(SaveCommand_Execute, ResultCommand_CanExecute);

        protected virtual async void SaveCommand_Execute()
        {          

        }

    }
}
