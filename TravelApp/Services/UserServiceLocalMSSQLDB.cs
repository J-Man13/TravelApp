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

        public UserEntity UpdateUserData(UserEntity user)
        {
            try
            {
                using (LocalTravelAppMSSQLDBContext localTravelAppMSSQLDBContext = new LocalTravelAppMSSQLDBContext())
                {
                    UserEntity ue = localTravelAppMSSQLDBContext.UserEntities.Find(user.UserName);
                    if (ue != null)
                    {
                        localTravelAppMSSQLDBContext.Entry(ue).CurrentValues.SetValues(user);
                        localTravelAppMSSQLDBContext.SaveChanges();
                    }
                    return ue;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }
    }
}