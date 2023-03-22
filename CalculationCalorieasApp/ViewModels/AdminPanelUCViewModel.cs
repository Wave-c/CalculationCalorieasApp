using CalculationCalorieasApp.Medels;
using CalculationCalorieasApp.Medels.Entitys;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationCalorieasApp.ViewModels
{
    public class AdminPanelUCViewModel : BindableBase
    {
        private string _name;
        private int _calories;

        public AdminPanelUCViewModel() 
        {
            Calories = 0;
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged();
                AddProductCommand.RaiseCanExecuteChanged();
                RemoveProductCommand.RaiseCanExecuteChanged();
            }
        }
        public int Calories
        {
            get => _calories;
            set
            {
                _calories = value;
                RaisePropertyChanged();
                AddProductCommand.RaiseCanExecuteChanged();
            }
        }

        private DelegateCommand _addProductCommand;
        public DelegateCommand AddProductCommand => _addProductCommand ??= new DelegateCommand(AddProductCommand_Execute, AddProductCommand_CanExecute);

        private DelegateCommand _removeProductCommand;
        public DelegateCommand RemoveProductCommand => _removeProductCommand ??= new DelegateCommand(RemoveProductCommand_Execute, RemoveProductCommand_CanExecute);

        private DelegateCommand _updateProductCommand;
        public DelegateCommand UpdateProductCommand => _updateProductCommand ??= new DelegateCommand(UpdateProductCommand_Execute);

        private DelegateCommand _saveProductCommand;
        public DelegateCommand SaveProductCommand => _saveProductCommand ??= new DelegateCommand(SaveProductCommand_Execute);

        private void SaveProductCommand_Execute()
        {

        }

        private void UpdateProductCommand_Execute()
        {

        }

        private async void RemoveProductCommand_Execute()
        {
            using (var dbContext = new AppDBContext())
            {
                dbContext.Remove(await dbContext.Products.Where(x => x.Name == Name).FirstAsync());
                await dbContext.SaveChangesAsync();
            }
            Name = "";
        }
        private bool RemoveProductCommand_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }

        private async void AddProductCommand_Execute()
        {
            using (var dbContext = new AppDBContext())
            {
                await dbContext.Products.AddAsync(new Product(Guid.NewGuid(), Name, Calories));
                await dbContext.SaveChangesAsync();
            }
            Name = "";
            Calories = 0;
        }
        private bool AddProductCommand_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(Name) && Calories != 0;
        }
    }
}
