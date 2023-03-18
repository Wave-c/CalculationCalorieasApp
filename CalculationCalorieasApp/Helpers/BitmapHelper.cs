﻿using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using CalculationCalorieasApp.Medels.Entitys;
using CalculationCalorieasApp.Medels;
using System.Linq;
using System.Text;

namespace CalculationCalorieasApp.Helpers
{
    public static class BitmapHelper
    {
        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
        public static Bitmap FromBitmapImagetoBitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }
        public static async Task<BitmapImage> GetUserImageAsync(User currentUser)
        {
            using (var dbContext = new AppDBContext())
            {
                byte[] image = dbContext.Users.Where(x => x.Id == currentUser.Id).First().Image;
                if (!string.IsNullOrWhiteSpace(Encoding.UTF8.GetString(image)))
                {
                    Bitmap img2;
                    using (var ms = new MemoryStream(image))
                    {
                        try
                        {
                            img2 = new Bitmap(ms);
                            return BitmapToBitmapImage(img2);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                {
                    return BitmapToBitmapImage(new Bitmap("..//..//..//Resource/NoPhotoUser.png"));
                }
                throw new Exception();
            }
        }
    }
}
