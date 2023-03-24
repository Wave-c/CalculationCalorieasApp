using CalculationCalorieasApp.Helpers;
using CalculationCalorieasApp.Medels;
using CalculationCalorieasApp.Medels.Entitys;
using CalculationCalorieasApp.Medels.Enums;
using CalculationCalorieasApp.Views;
using CalculationCalorieasApp.Views.Interfaces;
using Messager.Helpers;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CalculationCalorieasApp.ViewModels
{
    public class PersonalAccountWindowViewModel: RegWindowViewModel
    {
        User User { get; set; }
        public PersonalAccountWindowViewModel(ILoginOrRegisterWindow window, User user) : base(window)
        {
            User= user;          
                UserName=User.UserName;
                SelectedGender=User.Gender;
                SelectedGoal=User.Goal;
                SelectedActiv = User.Activ;
                Weight = User.Weight.ToString();
                Height = User.Height.ToString();
                Age = User.Age.ToString();
        }
        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand ??= new DelegateCommand(SaveCommand_Execute, SaveCommand_CanExecute);

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
                User.Gender = SelectedGender;
                User.Goal = SelectedGoal;
                User.Activ = SelectedActiv;
                User.Weight = Convert.ToInt32(Weight);
                User.Height = Convert.ToInt32(Height);
                User.Age = Convert.ToInt32(Age);
                User.CalPerDay = Convert.ToInt32(Result);

                dbContext.Entry(User).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                base.CloseWindow();

            }

        }
        private bool SaveCommand_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Weight) &&
                !string.IsNullOrWhiteSpace(Height) &&
            !string.IsNullOrWhiteSpace(Age);
        }

        protected override async void SaveLoginAndPasswordCommand_Execute()
        {
            using (var dbContext = new AppDBContext())
            {
                if (User.UserName != UserName)
                {
                    var currentUser = dbContext.Users.Where(x => x.UserName == UserName).FirstOrDefault();
                    if (currentUser != null)
                    {
                        MessageBox.Show("Такой пользователь уже существует, выберите другое имя", "Ошибка", MessageBoxButton.OK);
                        return;
                    }
                }
                if (User.Password!= Encryptor.GenerateHash(Password))
                {
                    MessageBox.Show("Неверный пароль, введите еще раз", "Ошибка", MessageBoxButton.OK);
                    return;
                }
                User.UserName = UserName;
                User.Password = Encryptor.GenerateHash(NewPassword);
                NewPassword=String.Empty;
                dbContext.Entry( User).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                base.CloseWindow();

            }
            base.SaveLoginAndPasswordCommand_Execute();

        }
        protected override bool SaveLoginAndPasswordCommand_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(UserName) &&
            !string.IsNullOrWhiteSpace(Password) &&
            !string.IsNullOrWhiteSpace(NewPassword);
        }

    }
}
