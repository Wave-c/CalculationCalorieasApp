﻿using CalculationCalorieasApp.Medels.Entitys;
using CalculationCalorieasApp.Medels.Enums;
using CalculationCalorieasApp.Medels.Extensions;
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
    /// Логика взаимодействия для PersonalAccountWindow.xaml
    /// </summary>
    public partial class PersonalAccountWindow : Window, ILoginOrRegisterWindow
    {
        private User _currentUser;

        public PersonalAccountWindow(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _goalComboBox.ItemsSource =
                (Enum.GetValues(typeof(Goal)) as Goal[])
                .Select(s => s.GetDescription());
            _genderComboBox.ItemsSource =
               (Enum.GetValues(typeof(Gender)) as Gender[])
               .Select(s => s.GetDescription());
            _activComboBox.ItemsSource =
               (Enum.GetValues(typeof(Activ)) as Activ[])
               .Select(s => s.GetDescription());
            DataContext = new PersonalAccountWindowViewModel(this,currentUser);
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((PersonalAccountWindowViewModel)DataContext).Password = _passwordBox.Password;
        }
        private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((PersonalAccountWindowViewModel)DataContext).NewPassword = _passwordBox.Password;
        }
    }

}
