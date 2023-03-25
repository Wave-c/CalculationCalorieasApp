using CalculationCalorieasApp.Medels;
using CalculationCalorieasApp.Medels.Entitys;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CalculationCalorieasApp.ViewModels
{
    public class AdminPanelUCViewModel : BindableBase
    {
        private Product _selectedProduct;
        private string _name;
        private int _calories;
        private string _updatedName;
        private MainWindowViewModel _parentViewModel;

        public AdminPanelUCViewModel(MainWindowViewModel parentViewModel) 
        {
            Calories = 0;
            _parentViewModel = parentViewModel;
            var dbContext = new AppDBContext();
            Products = new ObservableCollection<Product>(dbContext.Products);
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
        public string NameProduct
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
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                using (var dbContext = new AppDBContext())
                {
                    var productByName = dbContext.Products.Where(x => x.Id == _selectedProduct.Id).FirstOrDefault();
                    if (productByName != null)
                    {
                        Calories = productByName.Calories;
                        UpdatedName = productByName.Name;
                    }
                }
                RaisePropertyChanged();
                RemoveProductCommand.RaiseCanExecuteChanged();
            }
        }
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                RaisePropertyChanged();
                RemoveProductCommand.RaiseCanExecuteChanged();
            }
        }
        private DelegateCommand _addProductCommand;
        public DelegateCommand AddProductCommand => _addProductCommand ??= new DelegateCommand(AddProductCommand_Execute, AddProductCommand_CanExecute);

        private DelegateCommand _removeProductCommand;
        public DelegateCommand RemoveProductCommand => _removeProductCommand ??= new DelegateCommand(RemoveProductCommand_Execute, RemoveProductCommand_CanExecute);

        private DelegateCommand _updateProductCommand;
        public DelegateCommand UpdateProductCommand => _updateProductCommand ??= new DelegateCommand(UpdateProductCommand_Execute, UpdateProductCommand_CanExecute);

        private DelegateCommand _saveProductCommand;
        public DelegateCommand SaveProductCommand => _saveProductCommand ??= new DelegateCommand(SaveProductCommand_Execute);

        private void SaveProductCommand_Execute()
        {

        }

        private async void UpdateProductCommand_Execute()
        {
            using (var dbContext = new AppDBContext())
            {
                var updatedProduct = await dbContext.Products.Where(x => x.Id == SelectedProduct.Id).FirstOrDefaultAsync();
                    updatedProduct.Name = UpdatedName;
                    updatedProduct.Calories = Calories;
                    await dbContext.SaveChangesAsync();
                    await _parentViewModel.UpdateProductsAsync();
                
                Products = new ObservableCollection<Product>(dbContext.Products);
                UpdatedName = "";
                Calories = 0;
            }
        }
        private bool UpdateProductCommand_CanExecute()
        {
            return SelectedProduct != null && Calories != 0;
        }

        private async void RemoveProductCommand_Execute()
        {
            using (var dbContext = new AppDBContext())
            {
                var removedProduct = await dbContext.Products.Where(x => x.Id == SelectedProduct.Id).FirstAsync();
                    dbContext.Remove(removedProduct);
                    await dbContext.SaveChangesAsync();
                    await _parentViewModel.UpdateProductsAsync();
                    SelectedProduct = null;
                    Products = new ObservableCollection<Product>(dbContext.Products);
            }
        }
        private bool RemoveProductCommand_CanExecute()
        {
            return SelectedProduct!=null;
        }

        private async void AddProductCommand_Execute()
        {
            using (var dbContext = new AppDBContext())
            {
                await dbContext.Products.AddAsync(new Product(Guid.NewGuid(), NameProduct, Calories));
                await dbContext.SaveChangesAsync();
                await _parentViewModel.UpdateProductsAsync();
            }
            NameProduct = "";
            Calories = 0;
        }
        private bool AddProductCommand_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(NameProduct) && Calories != 0;
        }
    }
}
