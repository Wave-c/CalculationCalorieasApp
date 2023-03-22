using CalculationCalorieasApp.Medels.Entitys;
using CalculationCalorieasApp.Medels.Extensions;
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
        public CaloriesPerDayWindow(User user)
        {
            InitializeComponent();
            _goalComboBox.ItemsSource =
                (Enum.GetValues(typeof(Goal)) as Goal[])
                .Select(s => s.GetDescription());
            _genderComboBox.ItemsSource =
               (Enum.GetValues(typeof(Gender)) as Gender[])
               .Select(s => s.GetDescription());
            _activComboBox.ItemsSource =
               (Enum.GetValues(typeof(Activ)) as Activ[])
               .Select(s => s.GetDescription());
            DataContext = new CaloriesPerDayWindowViewModel(user);
        }
    }
}
