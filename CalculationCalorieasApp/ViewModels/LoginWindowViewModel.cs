using CalculationCalorieasApp.ViewModels.Base;
using CalculationCalorieasApp.Views;
using CalculationCalorieasApp.Views.Interfaces;
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

        protected override bool EnterToAppCommand_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(UserName);
        }
        private void OpenLoginRegWindowCommand_Execute()
        {
            var loginWindow = new LoginRegWindow();
            loginWindow.Show();
            ((LoginWindow)_window).Close();
        }
    }
}
