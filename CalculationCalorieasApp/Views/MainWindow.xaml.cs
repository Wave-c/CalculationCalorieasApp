using CalculationCalorieasApp.Helpers;
using CalculationCalorieasApp.Medels;
using CalculationCalorieasApp.Medels.Entitys;
using CalculationCalorieasApp.ViewModels;
using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculationCalorieasApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User _currentUser;
        public MainWindow(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            DataContext = new MainWindowViewModel(currentUser);
        }

        private void RadioButtonBreakfast_Checked(object sender, RoutedEventArgs e)
        {
            ((MainWindowViewModel)DataContext).Eating = Medels.Enums.Eating.BREAKFAST;
        }

        private void RadioButtonDinner_Checked(object sender, RoutedEventArgs e)
        {
            ((MainWindowViewModel)DataContext).Eating = Medels.Enums.Eating.DINNER;
        }

        private void RadioButtonSupper_Checked(object sender, RoutedEventArgs e)
        {
            ((MainWindowViewModel)DataContext).Eating = Medels.Enums.Eating.SUPPER;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _adminPanel.DataContext = new AdminPanelUCViewModel(((MainWindowViewModel)DataContext));
            await ((MainWindowViewModel)DataContext).UpdateProductsAsync();
            var temp = await BitmapHelper.GetUserImageAsync(_currentUser);
            ((MainWindowViewModel)DataContext).Image = temp;
            if (_currentUser.Status == StatusUser.USER)
            {
                this.MinHeight = 584;
                this.Height = 584;
                _mainGrid.RowDefinitions.Remove(_adminPanelRow);
            }
        }
    }
}
