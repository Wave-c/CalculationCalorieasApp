using CalculationCalorieasApp.Medels.Entitys;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using System.Windows;
using Prism.Mvvm;
using CalculationCalorieasApp.Helpers;

namespace CalculationCalorieasApp.ViewModels
{
    public class CaloriesPerDayWindowViewModel:BindableBase
    {
        User User { get; set; }
        public CaloriesPerDayWindowViewModel()
        {
           // User = user;
        }
        private Goal _selectedGoal;
        public Goal SelectedGoal
        {
            get => _selectedGoal;
            set
            {
                _selectedGoal = value;
                RaisePropertyChanged();
                ResultCommand.RaiseCanExecuteChanged();
            }
        }

        private Gender _selectedGender;
        public Gender SelectedGender
        {
            get => _selectedGender;
            set
            {
                _selectedGender = value;
                RaisePropertyChanged();
                ResultCommand.RaiseCanExecuteChanged();
            }
        }
        private Activ _selectedActiv;
        public Activ SelectedActiv
        {
            get => _selectedActiv;
            set
            {
                _selectedActiv = value;
                RaisePropertyChanged();
                ResultCommand.RaiseCanExecuteChanged();
            }
        }
        private string _weight;
        public string Weight
        {
            get => _weight;
            set
            {
                _weight = value;
                RaisePropertyChanged();
                ResultCommand.RaiseCanExecuteChanged();
            }
        }
        private string _height;
        public string Height
        {
            get => _height;
            set
            {
                _height = value;
                RaisePropertyChanged();
                ResultCommand.RaiseCanExecuteChanged();
            }
        }
        private string _age;
        public string Age
        {
            get => _age;
            set
            {
                _age = value;
                RaisePropertyChanged();
                ResultCommand.RaiseCanExecuteChanged();
            }
        }
        private string _result;
        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                RaisePropertyChanged();
                ResultCommand.RaiseCanExecuteChanged();
            }
        }
        private DelegateCommand _resultCommand;
        public DelegateCommand ResultCommand =>
                    _resultCommand ??= new DelegateCommand(ResultCommand_Execute, ResultCommand_CanExecute);

        public void ResultCommand_Execute()
        {
            int i = 0;
            if (SelectedGender == Gender.Man)
                i = 5;
            else i = -161;
            double result = ((9.99 * Convert.ToInt32(Weight)) + (6.25 * Convert.ToInt32(Height)) - (4.92 * Convert.ToInt32(Age)) + (i))* SwitchEnumHelper.EnumConverter(SelectedActiv);
            switch (SelectedGoal)
            {
                case Goal.Increase:
                    result += (result /100*20);
                    break;
                case Goal.Decrease:
                    result -= (result / 100 * 20);
                    break;
                case Goal.Save:
                    break;

            }
          
            Result= ((int)result).ToString();
        }
        public bool ResultCommand_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Weight) &&
                !string.IsNullOrWhiteSpace(Height) &&
            !string.IsNullOrWhiteSpace(Age);
        }
        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand =>
                    _saveCommand ??= new DelegateCommand(SaveCommand_Execute, SaveCommand_CanExecute);

        public void SaveCommand_Execute()
        {
            User.Activ = SelectedActiv;
            User.Gender = SelectedGender;
            User.Goal = SelectedGoal;
            User.Weight = Convert.ToInt32(Weight);
            User.Height= Convert.ToInt32(Height);
            User.Age= Convert.ToInt32(Age);
            User.CalPerDay = Convert.ToInt32(Result);

        }
        public bool SaveCommand_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Weight) &&
                !string.IsNullOrWhiteSpace(Height) &&
            !string.IsNullOrWhiteSpace(Age);
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
