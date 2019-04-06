using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TravelApp.Helpers;
using TravelApp.ViewModels;

namespace TravelApp.Views
{
    /// <summary>
    /// Interaction logic for AppMainView.xaml
    /// </summary>
    public partial class AppMainView : UserControl
    {
        private AppMainViewModel appMainViewModel;
        public AppMainView()
        {
            InitializeComponent();
            InitLoginImage();
            appMainViewModel = new AppMainViewModel();
            DataContext = appMainViewModel;
        }

        private void InitLoginImage()
        {
            using (Stream stream = File.Open(CurrentUserEntity.UserEntity.UserLoginImagePath, FileMode.Open))
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.StreamSource = stream;
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.EndInit();
                img.Freeze();
                LoginImage.ImageSource = img;
            }
        }

    }
}
