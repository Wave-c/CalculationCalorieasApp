using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

        private Gender _selectedGender;
        public Gender SelectedGender
        {
            get => _selectedGender;
            set
            {
                _selectedGender = value;
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
    public enum Gender
    {
        [Description("Мужчина")]
        Man,
        [Description("Женщина")]
        Woman
    }
    public enum Activ
    {
        [Description("Низкая активность, полное отсутствие спорта")]
        First,
        [Description("Малоподвижный образ жизни, физ. активность 1-2 раза в неделю")]
        Second,
        [Description("Средняя активность, физ. активность 2-4 раза в неделю")]
        Third,
        [Description("Активный образ жизни, подвижная работа, активность 5 раз в неделю")]
        Fourth,
        [Description("Высокая активность, подвижная работа, ежедневные тренировки")]
        Fifth
    }
}
