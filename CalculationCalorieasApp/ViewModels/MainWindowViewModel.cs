using CalculationCalorieasApp.Medels.Entitys;
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

        public MainWindowViewModel(User currentUser)
        {

        }

        public BitmapImage Image
        {
            get => _image;
            set
            {
                _image = value;
                RaisePropertyChanged();
            }
        }
    }
}
