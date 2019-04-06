using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TravelApp.Helpers;
using TravelApp.Models.EntityModels;
using TravelApp.Services;
using TravelApp.Views;
using TravelApp.Views.AppMainViewViews;

namespace TravelApp.ViewModels.AppMainViewModelViewModels
{
    public class UserEntityEditorViewModel : ViewModelBase
    {
        private string loginUpdate;
        public string LoginUpdate { get { return loginUpdate; } set { Set(ref loginUpdate, value); } }

        private string mailUpdate;
        public string MailUpdate { get { return mailUpdate; } set { Set(ref mailUpdate, value); } }

        private IUserService iUserService;
        private AppMainView appMainView;
 

        public UserEntityEditorViewModel()
        {
            iUserService = new UserServiceLocalMSSQLDB();
            LoginUpdate = "";
            MailUpdate = "";
        }

        public RelayCommand<Object> updateUser;
        public RelayCommand<Object> UpdateUser
        {
            get => updateUser ?? (updateUser = new RelayCommand<Object>(async obj => {
                (obj as UserEntityEditorView).UpdateButton.IsEnabled = false;


                //MessageBox.Show(((obj as UserEntityEditorView) as ContentControl).ToString());

                //((Application.Current.Windows[0] as AppView).CurrentPage as AppMainView).ToString();
               

                if (!Regex.IsMatch(MailUpdate, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                {
                    MessageBox.Show("Invalid email address format");
                    (obj as UserEntityEditorView).UpdateButton.IsEnabled = true;
                    return;
                }

                if (LoginUpdate.Trim().Length < 6)
                {
                    MessageBox.Show("Login's too short");
                    (obj as UserEntityEditorView).UpdateButton.IsEnabled = true;
                    return;
                }

                if ((obj as UserEntityEditorView).NewPassword.Password.Length < 6)
                {
                    MessageBox.Show("New password's too short");
                    (obj as UserEntityEditorView).UpdateButton.IsEnabled = true;
                    return;
                }
                

                if(!Md5Transformer.GetMd5String((obj as UserEntityEditorView).OldPassword.Password).Equals(CurrentUserEntity.UserEntity.PasswordHash))
                {
                    MessageBox.Show("Invalid old passowrd");
                    (obj as UserEntityEditorView).UpdateButton.IsEnabled = true;
                    return;
                }

                CurrentUserEntity.UserEntity.UserName = LoginUpdate.Trim();
                CurrentUserEntity.UserEntity.UserEmail = MailUpdate.Trim();
                CurrentUserEntity.UserEntity.PasswordHash = Md5Transformer.GetMd5String((obj as UserEntityEditorView).NewPassword.Password);

                bool hasBeenUpdated = false;
                await Task.Run(() => 
                {
                    hasBeenUpdated = iUserService.UpdateUserData(CurrentUserEntity.UserEntity.Id, CurrentUserEntity.UserEntity);
                    if (!hasBeenUpdated)
                        MessageBox.Show("Something went wrong on local DB service side");
                });

                if (hasBeenUpdated)
                {
                    MessageBox.Show("You will have to relog in");
                    (Application.Current.Windows[0] as AppView).Close();                    
                }

            }));
        }


    }
}
