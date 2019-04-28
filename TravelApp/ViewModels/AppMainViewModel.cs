using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.IO;
using System.Windows.Media.Imaging;
using TravelApp.Helpers;
using TravelApp.ViewModels.AppMainViewModelViewModels;
using TravelApp.Views;

namespace TravelApp.ViewModels
{
    public class AppMainViewModel : ViewModelBase
    {
        private String currentUserLogin;
        public String CurrentUserLogin { get { return currentUserLogin; } set {Set(ref currentUserLogin,value); } }

        private String contentTop;
        public String ContentTop { get { return contentTop; } set { Set(ref contentTop, value); } }

        private ViewModelBase currentContentPage;
        public ViewModelBase CurrentContentPage { get => currentContentPage; set => Set(ref currentContentPage, value); }

        public AppMainViewModel()
        {
            CurrentUserLogin = CurrentUserEntity.UserEntity.UserName;
            ContentTop = "Cities";
            CurrentContentPage = new CitiesViewModel();
        }

        private RelayCommand<Object> updatePicture;
        public RelayCommand<Object> UpdatePicture
        {
            get => updatePicture ?? (updatePicture = new RelayCommand<Object>(obj => {

                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.DefaultExt = ".png";
                dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                dlg.ShowDialog();

                string imagePath = dlg.FileName;
                if (String.IsNullOrEmpty(imagePath))
                    return;

                BitmapImage img;
                using (Stream stream = File.Open(imagePath, FileMode.Open))
                {
                    img = new BitmapImage();
                    img.BeginInit();
                    img.StreamSource = stream;
                    img.CacheOption = BitmapCacheOption.OnLoad;
                    img.EndInit();
                    img.Freeze();
                    (obj as AppMainView).LoginImage.ImageSource = img;
                }
                SaveImage(img, CurrentUserEntity.UserEntity.UserLoginImagePath);
            }));
        }

        private void SaveImage(BitmapImage image, string filePath)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate))
            {
                encoder.Save(fileStream);
            }
        }


        private RelayCommand<Object> logOut;
        public RelayCommand<Object> LogOut
        {
            get => logOut ?? (logOut = new RelayCommand<Object>(obj =>
            {
                CurrentUserEntity.UserEntity = null;
                (obj as AppMainView).Content = new LoginViewModel();                
            }));
        }

        private RelayCommand<object> goToUserEntity;
        public RelayCommand<object> GoToUserEntity
        {
            get => goToUserEntity ?? (goToUserEntity = new RelayCommand<object>(obj =>
            {
                ContentTop = CurrentUserLogin;
                CurrentContentPage = new UserEntityEditorViewModel();
            }));
        }

        private RelayCommand goToTrips;
        public RelayCommand GoToTrips
        {
            get => goToTrips ?? (goToTrips = new RelayCommand(() =>
            {
                ContentTop = "Trips";
                CurrentContentPage = new TripsViewModel();
            }));
        }

        private RelayCommand goToCities;
        public RelayCommand GoToCities
        {
            get => goToCities ?? (goToCities = new RelayCommand(() =>
            {
                ContentTop = "Cities";
                CurrentContentPage = new CitiesViewModel();
            }));
        }

    }
}
