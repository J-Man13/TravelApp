using System;
using System.Linq;
using System.Windows;
using TravelApp.Models.EntityModels;

namespace TravelApp.Services
{
    public class UserServiceLocalMSSQLDB : IUserService
    {
        public UserEntity RegistrateUser(UserEntity userEntity)
        {
            try
            {
                using (LocalTravelAppMSSQLDBContext localTravelAppMSSQLDBContext = new LocalTravelAppMSSQLDBContext())
                {
                    if (localTravelAppMSSQLDBContext.UserEntities.Where(u => u.UserName.Equals(userEntity.UserName)).
                        AsEnumerable().FirstOrDefault(u => u.UserName.Equals(userEntity.UserName)) != null)
                        return null;
                    UserEntity ue = localTravelAppMSSQLDBContext.UserEntities.Add(userEntity);
                    localTravelAppMSSQLDBContext.SaveChanges();
                    return ue;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }

        }

        public UserEntity FindUserByLogin(string Login)
        {
            try
            {
                using (LocalTravelAppMSSQLDBContext localTravelAppMSSQLDBContext = new LocalTravelAppMSSQLDBContext())
                {
      
                    return localTravelAppMSSQLDBContext.UserEntities.Where(u => u.UserName.Equals(Login)).AsEnumerable().FirstOrDefault(u => u.UserName.Equals(Login));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }


        public bool UpdateUserData(string UserLogin, UserEntity User)
        {
            try
            {
                using (LocalTravelAppMSSQLDBContext localTravelAppMSSQLDBContext = new LocalTravelAppMSSQLDBContext())
                {
                    UserEntity userToUpdate = localTravelAppMSSQLDBContext.UserEntities.Where(u => u.UserName.Equals(UserLogin))
                                              .AsEnumerable().First(u => u.UserName.Equals(UserLogin));
                    if (userToUpdate == null)
                        return false;
                    else
                    {
                        userToUpdate.PasswordHash = User.PasswordHash;
                        userToUpdate.UserName = User.UserName;
                        userToUpdate.UserEmail = User.UserEmail;
                        userToUpdate.UserLoginImagePath = User.UserLoginImagePath;
                        localTravelAppMSSQLDBContext.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        public bool UpdateUserData(long Id, UserEntity User)
        {
            try
            {
                using (LocalTravelAppMSSQLDBContext localTravelAppMSSQLDBContext = new LocalTravelAppMSSQLDBContext())
                {
                    UserEntity userToUpdate = localTravelAppMSSQLDBContext.UserEntities.Where(u => u.Id.Equals(Id)).FirstOrDefault();
                    if (userToUpdate == null)
                        return false;
                    else
                    {
                        userToUpdate.PasswordHash = User.PasswordHash;
                        userToUpdate.UserName = User.UserName;
                        userToUpdate.UserEmail = User.UserEmail;
                        userToUpdate.UserLoginImagePath = User.UserLoginImagePath;
                        localTravelAppMSSQLDBContext.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }

    }
}
