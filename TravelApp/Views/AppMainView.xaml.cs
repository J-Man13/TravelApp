using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TravelApp.Helpers;
using TravelApp.ViewModels;
using System.Windows.Media.Animation;
using System.Windows;

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

        private void btnLeftMenuHide_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbHideLeftMenu", btnLeftMenuHide, btnLeftMenuShow, pnlLeftMenu);
        }

        private void btnLeftMenuShow_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbShowLeftMenu", btnLeftMenuHide, btnLeftMenuShow, pnlLeftMenu);
        }

        private void ShowHideMenu(string Storyboard, Button btnHide, Button btnShow, StackPanel pnl)
        {
            Storyboard sb = Resources[Storyboard] as Storyboard;
            sb.Begin(pnl);

            if (Storyboard.Contains("Show"))
            {
                btnHide.Visibility = System.Windows.Visibility.Visible;
                btnShow.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (Storyboard.Contains("Hide"))
            {
                btnHide.Visibility = System.Windows.Visibility.Hidden;
                btnShow.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
