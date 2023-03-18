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
    public class LoginWindowViewModel : LoginAndRegisterViewModelBase
    {
        public LoginWindowViewModel(ILoginOrRegisterWindow window) : base(window)
        {
        }

        private void OpenLoginRegWindow()
        {
            var loginWindow = new LoginRegWindow();
            loginWindow.Show();
            ((LoginRegWindow)_window).Close();
        }
    }
}
