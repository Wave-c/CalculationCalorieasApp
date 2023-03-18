using CalculationCalorieasApp.Medels;
using CalculationCalorieasApp.Medels.Entitys;
using CalculationCalorieasApp.Views;
using CalculationCalorieasApp.Views.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationCalorieasApp.ViewModels.Base
{
    public class LoginAndRegisterViewModelBase : BindableBase
    {
        private string _userName;
        private string _password;
        protected ILoginOrRegisterWindow _window;

        public LoginAndRegisterViewModelBase(ILoginOrRegisterWindow window)
        {
            _window = window;
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        

        protected void EnterToApp()
        {
            using (var dbContext = new AppDBContext())
            {
                var currentUser = dbContext.Users.Where(x=>x.UserName == UserName).FirstOrDefault();
                var mainWindow = new MainWindow(currentUser);
            }
        }
    }
}
