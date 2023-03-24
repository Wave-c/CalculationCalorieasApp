using CalculationCalorieasApp.Medels.Entitys;
using CalculationCalorieasApp.Medels;
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
    public class LoginWindowViewModel : LoginAndRegisterViewModelBase
    {
        public LoginWindowViewModel(ILoginOrRegisterWindow window) : base(window)
        {
        }

        private DelegateCommand _openLoginRegWindowCommand;
        public DelegateCommand OpenLoginRegWindowCommand => _openLoginRegWindowCommand ??= new DelegateCommand(OpenLoginRegWindowCommand_Execute);

        protected override void EnterToAppCommand_Execute()
        {
            base.EnterToAppCommand_Execute();
        }
        protected override bool EnterToAppCommand_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(UserName);
        }

        private void OpenLoginRegWindowCommand_Execute()
        {
            var loginRegWindow = new LoginRegWindow();
            loginRegWindow.Show();
            ((LoginWindow)_window).Close();
        }

        protected override bool SaveLoginAndPasswordCommand_CanExecute()
        {
            throw new NotImplementedException();
        }
    }
}
