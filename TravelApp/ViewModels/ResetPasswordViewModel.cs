using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Threading.Tasks;
using System.Windows;
using TravelApp.Helpers;
using TravelApp.Models.EntityModels;
using TravelApp.Services;
using TravelApp.Views;

namespace TravelApp.ViewModels
{
    public class ResetPasswordViewModel : ViewModelBase
    {
        private IUserService userService;

        private string userLoginEmail;
        public string UserLoginEmail { get { return userLoginEmail; } set { Set(ref userLoginEmail, value); } }

        private RelayCommand<Object> goToLogInWindow;
        public RelayCommand<Object> GoToLogInWindow
        {
            get => goToLogInWindow ?? (goToLogInWindow = new RelayCommand<Object>(obj => {
                (obj as ResetPasswordView).Content = new LoginViewModel();
            }));
        }

        private RelayCommand<Object> changePassword;
        public RelayCommand<Object> ChangePassword
        {
            get => changePassword ?? (changePassword = new RelayCommand<Object>(async obj => {
                (obj as ResetPasswordView).UpdateButton.IsEnabled = false;
                UserEntity us = null;

                await Task.Run(() =>
                {
                    us = userService.FindUserByLogin(UserLoginEmail.Trim());
                });

                if (us == null)
                {
                    MessageBox.Show("No such user " + UserLoginEmail);
                    (obj as ResetPasswordView).UpdateButton.IsEnabled = true;
                    return;
                }

                if (!us.PasswordHash.Equals(Md5Transformer.GetMd5String((obj as ResetPasswordView).Password.Password)))
                {
                    MessageBox.Show("Invalid Password");
                    return;
                }

                if ((obj as ResetPasswordView).NewPassword.Password.Length < 6)
                {
                    MessageBox.Show("Password's too short");
                    (obj as ResetPasswordView).UpdateButton.IsEnabled = true;
                    return;
                }

                us.PasswordHash = Md5Transformer.GetMd5String((obj as ResetPasswordView).NewPassword.Password);
                if (userService.UpdateUserData(us.UserName, us))
                    MessageBox.Show("Password successfully updated");
                else
                    MessageBox.Show("Something went wrong");
                (obj as ResetPasswordView).UpdateButton.IsEnabled = true;

            }));
        }

        public ResetPasswordViewModel()
        {
            UserLoginEmail = "";
            userService = new UserServiceLocalMSSQLDB();
        }
    }
}
