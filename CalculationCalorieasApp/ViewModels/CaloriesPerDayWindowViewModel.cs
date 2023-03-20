using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationCalorieasApp.ViewModels
{
    public class CaloriesPerDayWindowViewModel
    {
        public CaloriesPerDayWindowViewModel()
        {

        }
        private Goal _selectedGoal;
        public Goal SelectedGoal
        {
            get => _selectedGoal;
            set
            {
                _selectedGoal = value;
                //RaisePropertyChanged();
                //SaveCommand.RaiseCanExecuteChanged();
            }
        }








    }
    public enum Goal
    {
        [Description("Увеличение веса")]
        Increase,
        [Description("Уменьшение веса")]
        Decrease,
        [Description("Сохранение веса")]
        Save
    }
    
}
