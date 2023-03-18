using CalculationCalorieasApp.ViewModels;
using CalculationCalorieasApp.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CalculationCalorieasApp.Views
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class LoginWindow : Window, ILoginOrRegisterWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginWindowViewModel(this);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            ((LoginWindowViewModel)DataContext).Password = passwordBox.Password;
        }
    }
}
