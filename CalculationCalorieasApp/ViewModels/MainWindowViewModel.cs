using CalculationCalorieasApp.Helpers;
using CalculationCalorieasApp.Medels.Entitys;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CalculationCalorieasApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private BitmapImage _image;
        private User _currentUser;

        public MainWindowViewModel(User currentUser)
        {
            _currentUser = currentUser;
            HasUserAdminOptions = currentUser.Status == StatusUser.ADMIN ? true : false;
        }

        public bool HasUserAdminOptions { get; set; }
        public BitmapImage Image
        {
            get => _image;
            set
            {
                _image = value;
                RaisePropertyChanged();
            }
        }

        private DelegateCommand _changeImageCommand;
        public DelegateCommand ChangeImageCommand => _changeImageCommand ??= new DelegateCommand(ChangeImageCommand_Execute);

        private async void ChangeImageCommand_Execute()
        {
            Image = await BitmapHelper.SetUserImageAsync(_currentUser);
        }
    }
}
