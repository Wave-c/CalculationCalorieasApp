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
using System.Windows;

namespace CalculationCalorieasApp.ViewModels
{
    public class AdminPanelUCViewModel : BindableBase
    {
        private string _name;
        private int _calories;
        private string _updatedName;
        private MainWindowViewModel _parentViewModel;

        public AdminPanelUCViewModel(MainWindowViewModel parentViewModel) 
        {
            Calories = 0;
            _parentViewModel = parentViewModel;
        }

        public string UpdatedName
        {
            get => _updatedName;
            set
            {
                _updatedName = value;
                RaisePropertyChanged();
                UpdateProductCommand.RaiseCanExecuteChanged();
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                using (var dbContext = new AppDBContext())
                {
                    var productByName = dbContext.Products.Where(x => x.Name == _name).FirstOrDefault();
                    if(productByName != null)
                    {
                        Calories = productByName.Calories;
                        UpdatedName = productByName.Name;
                    }
                }
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
        public DelegateCommand UpdateProductCommand => _updateProductCommand ??= new DelegateCommand(UpdateProductCommand_Execute, UpdateProductCommand_CanExecute);

        private async void UpdateProductCommand_Execute()
        {
            using (var dbContext = new AppDBContext())
            {
                var updatedProduct = await dbContext.Products.Where(x => x.Name == Name).FirstOrDefaultAsync();
                if(updatedProduct != null)
                {
                    updatedProduct.Name = UpdatedName;
                    updatedProduct.Calories = Calories;
                    await dbContext.SaveChangesAsync();
                    await _parentViewModel.UpdateProducts();
                }
                Name = "";
                UpdatedName = "";
                Calories = 0;
            }
        }
        private bool UpdateProductCommand_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(UpdatedName) && Calories != 0;
        }

        private async void RemoveProductCommand_Execute()
        {
            using (var dbContext = new AppDBContext())
            {
                var removedProduct = await dbContext.Products.Where(x => x.Name == Name).FirstAsync();
                if(removedProduct != null)
                {
                    dbContext.Remove(removedProduct);
                    await dbContext.SaveChangesAsync();
                    await _parentViewModel.UpdateProducts();
                }
                else
                {
                    MessageBox.Show("Такого продукта не существует", "Error", MessageBoxButton.OK);
                }
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
                await _parentViewModel.UpdateProducts();
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
