using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Threading.Tasks;
using System.Windows;
using TravelApp.Helpers;
using TravelApp.Models.EntityModels;
using TravelApp.Services;
using TravelApp.Views;

namespace TravelApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string userLoginEmail;
        public string UserLoginEmail { get { return userLoginEmail; } set { Set(ref userLoginEmail,value); } }

        private IUserService userService;

        private RelayCommand<Object> goToSignInWindow;
        public RelayCommand<Object> GoToSignInWindow
        {
            get => goToSignInWindow ?? (goToSignInWindow = new RelayCommand<Object>(obj => {                
                (obj as LoginView).Content = new SignUpViewModel();
            }));
        }

        private RelayCommand<Object> goToResetPassword;
        public RelayCommand<Object> GoToResetPassword
        {
            get => goToResetPassword ?? (goToResetPassword = new RelayCommand<Object>(obj => {                
                (obj as LoginView).Content = new ResetPasswordViewModel();
            }));
        }

        private RelayCommand<Object> tryLogin;
        public RelayCommand<Object> TryLogin
        {
            get => tryLogin ?? (tryLogin = new RelayCommand<Object>(async obj =>
            {
                (obj as LoginView).LoginButton.IsEnabled = false;

                UserEntity us = null;

                await Task.Run(() =>
                {
                    us = userService.FindUserByLogin(UserLoginEmail.Trim());
                });

                if (us == null)
                {
                    MessageBox.Show("No such user " + UserLoginEmail);
                    (obj as LoginView).LoginButton.IsEnabled = true;
                    return;
                }

                if (us.PasswordHash != (Md5Transformer.GetMd5String((obj as LoginView).Password.Password)))
                {
                    MessageBox.Show("Invalid Password");
                    (obj as LoginView).LoginButton.IsEnabled = true;
                    return;
                }


                CurrentUserEntity.UserEntity = us;
                (obj as LoginView).Content = new AppMainViewModel();
            }));
        }


        public LoginViewModel()
        {
            UserLoginEmail = "";
            userService = new UserServiceLocalMSSQLDB();
        }
    }
}
