using CalculationCalorieasApp.Helpers;
using CalculationCalorieasApp.Medels;
using CalculationCalorieasApp.Medels.Entitys;
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
            using (var dbContext = new AppDBContext())
            {
                UserName=User.UserName;
                SelectedGender=User.Gender;
                SelectedGoal=User.Goal;
                SelectedActiv = User.Activ;
                Weight = User.Weight.ToString();
                Height = User.Height.ToString();
                Age = User.Age.ToString();
            }
        }
        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand ??= new DelegateCommand(SaveCommand_Execute);

        private async void SaveCommand_Execute()
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
                //var addedUser = new User()
                //{
                //Id = Guid.NewGuid(),
                User.UserName = UserName;
                //Password = Encryptor.GenerateHash(Password),
                //User.Status = StatusUser.USER,
                //Image = new byte[0],
                User.Gender = SelectedGender;
                User.Goal = SelectedGoal;
                User.Activ = SelectedActiv;
                User.Weight = Convert.ToInt32(Weight);
                User.Height = Convert.ToInt32(Height);
                User.Age = Convert.ToInt32(Age);
                User.CalPerDay = Convert.ToInt32(Result);

                //};
                dbContext.Entry(User).State=EntityState.Modified;
                await dbContext.SaveChangesAsync();

                //base.EnterToAppCommand_Execute();

            }

        }

    }
}
