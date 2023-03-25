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
using CalculationCalorieasApp.Medels;
using CalculationCalorieasApp.Medels.Enums;
using CalculationCalorieasApp.Views;
using CalculationCalorieasApp.ViewModels.Base;
using CalculationCalorieasApp.Views.Interfaces;

namespace CalculationCalorieasApp.ViewModels
{
    public class CaloriesPerDayWindowViewModel:CaloriesPerDayAndPersonalAccountViewModelBase
    {
        public CaloriesPerDayWindowViewModel(User user, ILoginOrRegisterWindow _window): base(_window, user)
        {
        }

        protected override bool ResultCommand_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Weight) &&
                !string.IsNullOrWhiteSpace(Height) &&
            !string.IsNullOrWhiteSpace(Age);
        }

        protected override bool RegisterCommand_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Weight) &&
                !string.IsNullOrWhiteSpace(Height) &&
            !string.IsNullOrWhiteSpace(Age)&&
            !string.IsNullOrWhiteSpace(Result);
        }

        protected override bool SaveLoginAndPasswordCommand_CanExecute()
        {
            throw new NotImplementedException();
        }
    }
}
