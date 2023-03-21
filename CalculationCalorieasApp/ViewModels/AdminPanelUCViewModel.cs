using CalculationCalorieasApp.Medels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationCalorieasApp.ViewModels
{
    public class AdminPanelUCViewModel
    {
        private DelegateCommand _addProductCommand;
        public DelegateCommand AddProductCommand => _addProductCommand ??= new DelegateCommand(AddProductCommand_Execute);

        private DelegateCommand _removeProductCommand;
        public DelegateCommand RemoveProductCommand => _removeProductCommand ??= new DelegateCommand(RemoveProductCommand_Execute);

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
        private void RemoveProductCommand_Execute()
        {

        }
        private void AddProductCommand_Execute()
        {
            //using(var dbContext = new AppDBContext())
            //{
            //    dbContext.Products.Add()
            //}
        }
    }
}
