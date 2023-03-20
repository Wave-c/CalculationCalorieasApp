﻿using CalculationCalorieasApp.Medels.Extensions;
using CalculationCalorieasApp.ViewModels;
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
    /// Логика взаимодействия для CaloriesPerDayWindow.xaml
    /// </summary>
    public partial class CaloriesPerDayWindow : Window
    {
        public CaloriesPerDayWindow()
        {
            InitializeComponent();
            _statusComboBox.ItemsSource =
                (Enum.GetValues(typeof(Goal)) as Goal[])
                .Select(s => s.GetDescription());
            DataContext = new CaloriesPerDayWindowViewModel();
        }
    }
}
