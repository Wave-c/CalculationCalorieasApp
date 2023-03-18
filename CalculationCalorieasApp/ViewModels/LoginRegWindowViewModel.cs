using CalculationCalorieasApp.ViewModels.Base;
using CalculationCalorieasApp.Views;
using CalculationCalorieasApp.Views.Interfaces;
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
        public LoginRegWindowViewModel(ILoginOrRegisterWindow window) : base(window)
        {
        }

        private void OpenLoginWindow()
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            ((LoginWindow)_window).Close();
        }
    }
}
