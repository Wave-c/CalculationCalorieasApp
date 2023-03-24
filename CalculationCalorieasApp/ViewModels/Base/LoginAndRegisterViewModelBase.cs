using CalculationCalorieasApp.Medels;
using CalculationCalorieasApp.Medels.Entitys;
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

namespace CalculationCalorieasApp.ViewModels.Base
{
    public abstract class LoginAndRegisterViewModelBase : BindableBase
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
                EnterToAppCommand.RaiseCanExecuteChanged();
                SaveLoginAndPasswordCommand.RaiseCanExecuteChanged();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged();
                EnterToAppCommand.RaiseCanExecuteChanged();
                SaveLoginAndPasswordCommand.RaiseCanExecuteChanged();
            }
        }
        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                RaisePropertyChanged();
                SaveLoginAndPasswordCommand.RaiseCanExecuteChanged();
            }
        }
        protected DelegateCommand _enterToAppCommand;
        public DelegateCommand EnterToAppCommand => _enterToAppCommand ??= new DelegateCommand(EnterToAppCommand_Execute, EnterToAppCommand_CanExecute);

        protected abstract bool EnterToAppCommand_CanExecute();
        protected virtual void EnterToAppCommand_Execute()
        {
            using (var dbContext = new AppDBContext())
            {
                var currentUser = dbContext.Users.Where(x=>x.UserName == UserName && x.Password == Encryptor.GenerateHash(Password)).FirstOrDefault();
                if (currentUser == null)
                {
                    MessageBox.Show("Неверное имя пользователя или пароль", "Ошибка", MessageBoxButton.OK);
                    return;
                }

                var mainWindow = new MainWindow(currentUser);
                mainWindow.Show();
            }
            ((Window)_window).Close();
        }

        protected DelegateCommand _saveLoginAndPasswordCommand;
        public DelegateCommand SaveLoginAndPasswordCommand => _saveLoginAndPasswordCommand ??= new DelegateCommand(SaveLoginAndPasswordCommand_Execute, SaveLoginAndPasswordCommand_CanExecute);

        protected abstract bool SaveLoginAndPasswordCommand_CanExecute();
        protected virtual void SaveLoginAndPasswordCommand_Execute()
        {            
            ((Window)_window).Close();
        }
        public void CloseWindow()
        {
            ((Window)_window).Close();
        }
    }
}
