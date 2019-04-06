using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using TravelApp.Helpers;
using TravelApp.Models.EntityModels;
using TravelApp.Services;
using TravelApp.Views;

namespace TravelApp.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private IUserService iUserService;


        private string loginSignUp;
        public string LoginSignUp { get { return loginSignUp; } set {Set(ref loginSignUp,value); } }

        private string mailSignUp;
        public string MailSignUp { get { return mailSignUp; } set { Set(ref mailSignUp, value); } }

        private  BitmapImage img;
        private string imagePath;

        private RelayCommand<Object> registerUser;
        public RelayCommand<Object> RegisterUser
        {
            get => registerUser ?? (registerUser = new RelayCommand<Object>( async obj => {
                (obj as SignUpView).SignUpButton.IsEnabled = false;

                if (!(obj as SignUpView).PasswordBoxesAreEqual()){
                    MessageBox.Show("Passwords are not equal");
                    (obj as SignUpView).SignUpButton.IsEnabled = true;
                    return;
                }

                if(!Regex.IsMatch(MailSignUp, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                {
                    MessageBox.Show("Invalid email address format");
                    (obj as SignUpView).SignUpButton.IsEnabled = true;
                    return;
                }

                if (LoginSignUp.Trim().Length < 6)
                {
                    MessageBox.Show("Login's too short");
                    (obj as SignUpView).SignUpButton.IsEnabled = true;
                    return;
                }

                if ((obj as SignUpView).Password.Password.Length<6){
                    MessageBox.Show("Password's too short");
                    (obj as SignUpView).SignUpButton.IsEnabled = true;
                    return;
                }

                if(img == null || string.IsNullOrEmpty(imagePath))
                {
                    MessageBox.Show("Image is not set");
                    (obj as SignUpView).SignUpButton.IsEnabled = true;
                    return;
                }

                UserEntity user = null;
                await  Task.Run(() => 
                {
                    user = new UserEntity()
                    {
                        PasswordHash = Md5Transformer.GetMd5String((obj as SignUpView).Password.Password),
                        UserEmail = MailSignUp,
                        UserName = LoginSignUp.Trim(),
                        UserLoginImagePath = Directory.GetCurrentDirectory() + "\\login_images\\" + LoginSignUp.Trim() + ".png"
                    };

                    user = iUserService.RegistrateUser(user);
                    if(user != null && user.Id!=0 && user.Id != -1)
                    {                        
                        CurrentUserEntity.UserEntity = user;
                        SaveImage(img, "login_images\\" + loginSignUp.Trim() + ".png");
                    }
                });

                if (user != null && user.Id != 0 && user.Id != -1)
                {
                    SaveImage(img, "login_images\\" + loginSignUp.Trim() + ".png");
                    //Application.Current.Dispatcher.Invoke(() => (obj as SignUpView).Content = new AppMainViewModel());
                    (obj as SignUpView).Content = new AppMainViewModel();
                }
                else
                {
                    MessageBox.Show("Unable to registrate because of login duplication");
                    (obj as SignUpView).SignUpButton.IsEnabled = true;
                }
            }));
        }
        
        private RelayCommand<Object> goToLogInWindow;
        public RelayCommand<Object> GoToLogInWindow
        {
            get => goToLogInWindow ?? (goToLogInWindow = new RelayCommand<Object>(obj => {
                (obj as SignUpView).Content = new LoginViewModel();
            }));
        }


        private RelayCommand<Object> addPicture;
        public RelayCommand<Object> AddPicture
        {
            get => addPicture ?? (addPicture = new RelayCommand<Object>(obj => {

                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.DefaultExt = ".png";
                dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                dlg.ShowDialog();

                imagePath = dlg.FileName;
                if (String.IsNullOrEmpty(imagePath))
                    return;

                using (Stream stream = File.Open(imagePath, FileMode.Open))
                {
                    img = new BitmapImage();
                    img.BeginInit();
                    img.StreamSource = stream;
                    img.CacheOption = BitmapCacheOption.OnLoad;
                    img.EndInit();
                    img.Freeze();
                    (obj as SignUpView).SignUpImage.ImageSource = img;
                }
            }));
        }

        public void SaveImage(BitmapImage image, string filePath)
        {
            if (!Directory.Exists("login_images"))
                Directory.CreateDirectory("login_images");

            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));

            using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.OpenOrCreate))
            {                
                encoder.Save(fileStream);
            }
        }

        public SignUpViewModel()
        {
            LoginSignUp = "";
            MailSignUp = "";
            iUserService = new UserServiceLocalMSSQLDB();
        }

    }
}