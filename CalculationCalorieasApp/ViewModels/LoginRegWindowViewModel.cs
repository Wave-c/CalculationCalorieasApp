using CalculationCalorieasApp.Medels;
using CalculationCalorieasApp.Medels.Entitys;
using CalculationCalorieasApp.ViewModels.Base;
using CalculationCalorieasApp.Views;
using CalculationCalorieasApp.Views.Interfaces;
using Messager.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CalculationCalorieasApp.ViewModels
{
    public class LoginRegWindowViewModel : LoginAndRegisterViewModelBase
    {
        private string _email;

        public LoginRegWindowViewModel(ILoginOrRegisterWindow window) : base(window)
        {
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged();
                EnterToAppCommand.RaiseCanExecuteChanged();
            }
        }

        private DelegateCommand _openLoginWindowCommand;
        public DelegateCommand OpenLoginWindowCommand => _openLoginWindowCommand ??= new DelegateCommand(OpenLoginWindowCommand_Execute);

        protected async override void EnterToAppCommand_Execute()
        {
            var addedUser = new User()
            {
                Id = Guid.NewGuid(),
                UserName = UserName,
                Password = Encryptor.GenerateHash(Password),
                Status = StatusUser.USER,
                Image = new byte[0],
                Email = Email
            };
            using (var dbContext = new AppDBContext())
            {
                if(dbContext.Users.Where(x=>x.UserName == addedUser.UserName).FirstOrDefault() != null)
                {
                    MessageBox.Show("Такой пользователь уже существует, выберите другое имя", "Ошибка", MessageBoxButton.OK);
                    return;
                }
                await dbContext.Users.AddAsync(addedUser);
                await dbContext.SaveChangesAsync();
            }
            base.EnterToAppCommand_Execute();
        }
        protected override bool EnterToAppCommand_CanExecute()
        {
            if(!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password))
            {
                return true;
            }
            return false;
        }

        private void OpenLoginWindowCommand_Execute()
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            ((LoginRegWindow)_window).Close();
        }


    }
}
