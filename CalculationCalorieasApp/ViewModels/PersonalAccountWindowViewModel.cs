using CalculationCalorieasApp.Helpers;
using CalculationCalorieasApp.Medels;
using CalculationCalorieasApp.Medels.Entitys;
using CalculationCalorieasApp.Medels.Enums;
using CalculationCalorieasApp.ViewModels.Base;
using CalculationCalorieasApp.Views;
using CalculationCalorieasApp.Views.Interfaces;
using Messager.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace CalculationCalorieasApp.ViewModels
{
    public class PersonalAccountWindowViewModel : CaloriesPerDayAndPersonalAccountViewModelBase
    {
        User User { get; set; }
        public  PersonalAccountWindowViewModel(ILoginOrRegisterWindow window, User user) : base(window, user)
        {
            User = user;
            UserName = User.UserName;
            SelectedGender = User.Gender;
            SelectedGoal = User.Goal;
            SelectedActiv = User.Activ;
            Weight = User.Weight.ToString();
            Height = User.Height.ToString();
            Age = User.Age.ToString();
        }
        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                RaisePropertyChanged();
                SaveLoginAndPasswordCommand.RaiseCanExecuteChanged();
            }
        }
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged();
                SaveLoginAndPasswordCommand.RaiseCanExecuteChanged();
            }
        }
        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                RaisePropertyChanged();
                SaveLoginAndPasswordCommand.RaiseCanExecuteChanged();
            }
        }
        private BitmapImage _image;
        public BitmapImage Image
        {
            get => _image;
            set
            {
                _image = value;
                RaisePropertyChanged();
                ChangeImageCommand.RaiseCanExecuteChanged();
            }
        }
        protected override async void SaveCommand_Execute()
        {
            using (var dbContext = new AppDBContext())
            {
               base.ResultCommand_Execute();
                User.Gender = SelectedGender;
                User.Goal = SelectedGoal;
                User.Activ = SelectedActiv;
                User.Weight = Convert.ToInt32(Weight);
                User.Height = Convert.ToInt32(Height);
                User.Age = Convert.ToInt32(Age);
                User.CalPerDay = Convert.ToInt32(Result);

                dbContext.Entry(User).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                base.CloseWindow();

            }

        }

        protected override async void SaveLoginAndPasswordCommand_Execute()
        {
            using (var dbContext = new AppDBContext())
            {
                if (User.UserName != UserName)
                {
                    var currentUser = dbContext.Users.Where(x => x.UserName == UserName).FirstOrDefault();
                    if (currentUser != null)
                    {
                        MessageBox.Show("Такой пользователь уже существует, выберите другое имя", "Ошибка", MessageBoxButton.OK);
                        return;
                    }
                }
                if (User.Password != Encryptor.GenerateHash(Password))
                {
                    MessageBox.Show("Неверный пароль, введите еще раз", "Ошибка", MessageBoxButton.OK);
                    return;
                }
                User.UserName = UserName;
                User.Password = Encryptor.GenerateHash(NewPassword);
                NewPassword = String.Empty;
                dbContext.Entry(User).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                base.CloseWindow();

            }
            base.SaveLoginAndPasswordCommand_Execute();

        }
        protected override bool SaveLoginAndPasswordCommand_CanExecute()
        {
            return !string.IsNullOrWhiteSpace(UserName) &&
            !string.IsNullOrWhiteSpace(Password) &&
            !string.IsNullOrWhiteSpace(NewPassword);
        }
        private DelegateCommand _changeImageCommand;
        public DelegateCommand ChangeImageCommand => _changeImageCommand ??= new DelegateCommand(ChangeImageCommand_Execute);
        private async void ChangeImageCommand_Execute()
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";

            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                Image = BitmapHelper.BitmapToBitmapImage(new Bitmap(filename));

                ImageConverter converter = new ImageConverter();
                byte[] bTemp = (byte[])converter.ConvertTo(BitmapHelper.FromBitmapImagetoBitmap(Image), typeof(byte[]));
                using (var dbContext = new AppDBContext())
                {
                    User.Image = bTemp;
                    dbContext.Entry(User).State = EntityState.Modified;
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        protected override bool RegisterCommand_CanExecute()
        {
            return true;
        }

        protected override bool ResultCommand_CanExecute()
        {
            return true;
        }
    }
}
