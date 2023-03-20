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
    /// Логика взаимодействия для LoginReg.xaml
    /// </summary>
    public partial class LoginRegWindow : Window, ILoginOrRegisterWindow
    {
        public LoginRegWindow()
        {
            InitializeComponent();
            DataContext = new LoginRegWindowViewModel(this);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((LoginRegWindowViewModel)DataContext).Password = _passwordBox.Password;
        }
    }
}
