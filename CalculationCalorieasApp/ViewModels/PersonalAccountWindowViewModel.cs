using CalculationCalorieasApp.Helpers;
using CalculationCalorieasApp.Medels;
using CalculationCalorieasApp.Medels.Entitys;
using CalculationCalorieasApp.Medels.Enums;
using CalculationCalorieasApp.ViewModels.Base;
using CalculationCalorieasApp.Views;
using CalculationCalorieasApp.Views.Interfaces;
using Messager.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace CalculationCalorieasApp.ViewModels
{
    public class PersonalAccountWindowViewModel : BindableBase
    {
        private User _user;
        private ILoginOrRegisterWindow _window;
        public PersonalAccountWindowViewModel(ILoginOrRegisterWindow window, User user)
        {
            _window = window;
            _user = user;
            UserName = _user.UserName;
            SelectedGender = _user.Gender;
            SelectedGoal = _user.Goal;
            SelectedActiv = _user.Activ;
            Weight = _user.Weight.ToString();
            Height = _user.Height.ToString();
            Age = _user.Age.ToString();
        }
        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged();
                SaveLoginAndPasswordCommand.RaiseCanExecuteChanged();
            }
        }
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged();
                SaveLoginAndPasswordCommand.RaiseCanExecuteChanged();
            }
        }
        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                RaisePropertyChanged();
                SaveLoginAndPasswordCommand.RaiseCanExecuteChanged();
            }
        }
        private BitmapImage _image;
        public BitmapImage Image
        {
            get => _image;
            set
            {
                _image = value;
                RaisePropertyChanged();
                ChangeImageCommand.RaiseCanExecuteChanged();
            }
        }
        private Goal _selectedGoal;
        public Goal SelectedGoal
        {
            get => _selectedGoal;
            set
            {
                _selectedGoal = value;
                RaisePropertyChanged();
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
            }
        }

        private DelegateCommand _saveLoginAndPasswordCommand;
        public DelegateCommand SaveLoginAndPasswordCommand => _saveLoginAndPasswordCommand ??= new DelegateCommand(SaveLoginAndPasswordCommand_Execute, SaveLoginAndPasswordCommand_CanExecute);

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand ??= new DelegateCommand(SaveCommand_Execute);

        private async void SaveCommand_Execute()
        {
            using (var dbContext = new AppDBContext())
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
                _user.Gender = SelectedGender;
                _user.Goal = SelectedGoal;
                _user.Activ = SelectedActiv;
                _user.Weight = Convert.ToInt32(Weight);
                _user.Height = Convert.ToInt32(Height);
                _user.Age = Convert.ToInt32(Age);
                _user.CalPerDay = Convert.ToInt32(Result);

                dbContext.Entry(_user).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();

                ((Window)_window).Close();
            }

        }

        private async void SaveLoginAndPasswordCommand_Execute()
        {
            using (var dbContext = new AppDBContext())
            {
                if (_user.UserName != UserName)
                {
                    var currentUser = dbContext.Users.Where(x => x.UserName == UserName).FirstOrDefault();
                    if (currentUser != null)
                    {
                        MessageBox.Show("Такой пользователь уже существует, выберите другое имя", "Ошибка", MessageBoxButton.OK);
                        return;
                    }
                }
                if (_user.Password != Encryptor.GenerateHash(Password))
                {
                    MessageBox.Show("Неверный пароль, введите еще раз", "Ошибка", MessageBoxButton.OK);
                    return;
                }
                _user.UserName = UserName;
                _user.Password = Encryptor.GenerateHash(NewPassword);
                NewPassword = String.Empty;
                dbContext.Entry(_user).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                ((Window)_window).Close();

            }
            ((Window)_window).Close();

        }
        private bool SaveLoginAndPasswordCommand_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(UserName) &&
            !string.IsNullOrWhiteSpace(Password) &&
            !string.IsNullOrWhiteSpace(NewPassword);
        }
        private DelegateCommand _changeImageCommand;
        public DelegateCommand ChangeImageCommand => _changeImageCommand ??= new DelegateCommand(ChangeImageCommand_Execute);
        private async void ChangeImageCommand_Execute()
        {
            Image = await BitmapHelper.SetUserImageAsync(_user);
        }
    }
}

