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
using CalculationCalorieasApp.Views;
using CalculationCalorieasApp.ViewModels.Base;
using CalculationCalorieasApp.Views.Interfaces;
using Messager.Helpers;

namespace CalculationCalorieasApp.ViewModels
{
    public class RegWindowViewModel : LoginAndRegisterViewModelBase
    {
        private string _passwordConfirmation;
        public RegWindowViewModel(ILoginOrRegisterWindow window) : base(window)
        {        
        }
        private Goal _selectedGoal;
        public Goal SelectedGoal
        {
            get => _selectedGoal;
            set
            {
                _selectedGoal = value;
                RaisePropertyChanged();
                EnterToAppCommand.RaiseCanExecuteChanged();
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
                EnterToAppCommand.RaiseCanExecuteChanged();
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
                EnterToAppCommand.RaiseCanExecuteChanged();
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
                EnterToAppCommand.RaiseCanExecuteChanged();
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
                EnterToAppCommand.RaiseCanExecuteChanged();
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
                EnterToAppCommand.RaiseCanExecuteChanged();
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
                EnterToAppCommand.RaiseCanExecuteChanged();
            }
        }
        public string PasswordConfirmation
        {
            get => _passwordConfirmation;
            set
            {
                _passwordConfirmation = value;
                RaisePropertyChanged();
                EnterToAppCommand.RaiseCanExecuteChanged();
            }
        }
        private DelegateCommand _openLoginWindowCommand;
        public DelegateCommand OpenLoginWindowCommand => _openLoginWindowCommand ??= new DelegateCommand(OpenLoginWindowCommand_Execute);

        protected async override void EnterToAppCommand_Execute()
        {
            using (var dbContext = new AppDBContext())
            {
                var currentUser = dbContext.Users.Where(x => x.UserName == UserName).FirstOrDefault();
                if (currentUser != null)
                {
                    MessageBox.Show("Такой пользователь уже существует, выберите другое имя", "Ошибка", MessageBoxButton.OK);
                    return;
                }
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
                var addedUser = new User()
                {
                    Id = Guid.NewGuid(),
                    UserName = UserName,
                    Password = Encryptor.GenerateHash(Password),
                    Status = StatusUser.USER,
                    Image = new byte[0],
                    Gender = SelectedGender,
                    Goal = SelectedGoal,
                    Activ = SelectedActiv,
                    Weight = Convert.ToInt32(Weight),
                    Height = Convert.ToInt32(Height),
                    Age = Convert.ToInt32(Age),
                    CalPerDay = Convert.ToInt32(Result)

                };
                    await dbContext.Users.AddAsync(addedUser);
                    await dbContext.SaveChangesAsync();

                base.EnterToAppCommand_Execute();

            }
            
        }
        protected override bool EnterToAppCommand_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Weight) &&
                !string.IsNullOrWhiteSpace(Height) &&
            !string.IsNullOrWhiteSpace(Age) &&
            !string.IsNullOrWhiteSpace(UserName) &&
            !string.IsNullOrWhiteSpace(Password);         
        }

        private void OpenLoginWindowCommand_Execute()
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            ((RegWindow)_window).Close();
        }

    }
    public enum Goal
    {
        [Description("Увеличение веса")]
        Increase,
        [Description("Уменьшение веса")]
        Decrease,
        [Description("Сохранение веса")]
        Save
    }
    public enum Gender
    {
        [Description("Мужчина")]
        Man,
        [Description("Женщина")]
        Woman,
        [Description("Пол не указан")]
        No
    }
    public enum Activ
    {
        [Description("Низкая активность, полное отсутствие спорта")]
        First,
        [Description("Малоподвижный образ жизни, физ. активность 1-2 раза в неделю")]
        Second,
        [Description("Средняя активность, физ. активность 2-4 раза в неделю")]
        Third,
        [Description("Активный образ жизни, подвижная работа, активность 5 раз в неделю")]
        Fourth,
        [Description("Высокая активность, подвижная работа, ежедневные тренировки")]
        Fifth
    }
}
