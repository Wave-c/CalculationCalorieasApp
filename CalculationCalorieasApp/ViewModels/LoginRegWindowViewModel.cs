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

namespace CalculationCalorieasApp.ViewModels
{
    public class LoginRegWindowViewModel : LoginAndRegisterViewModelBase
    {
        private string _passwordConfirmation;

        public LoginRegWindowViewModel(ILoginOrRegisterWindow window) : base(window)
        {
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
            var addedUser = new User()
            {
                Id = Guid.NewGuid(),
                UserName = UserName,
                Password = Encryptor.GenerateHash(Password),
                Status = StatusUser.USER,
                Image = new byte[0],

            };
            using (var dbContext = new AppDBContext())
            {
                await dbContext.Users.AddAsync(addedUser);
                await dbContext.SaveChangesAsync();
            }
            base.EnterToAppCommand_Execute();
        }
        protected override bool EnterToAppCommand_CanExecute()
        {
            if(/*Password == PasswordConfirmation && */!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password))
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
