using System;
using System.Linq;
using System.Windows;
using TravelApp.Models.EntityModels;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

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
                    ue.TripEntities = new HashSet<TripEntity>();
                    localTravelAppMSSQLDBContext.SaveChanges();
                    return ue;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public UserEntity FindUserByLogin(string Login)
        {
            try
            {
                using (LocalTravelAppMSSQLDBContext localTravelAppMSSQLDBContext = new LocalTravelAppMSSQLDBContext())
                {      
                    UserEntity ue = localTravelAppMSSQLDBContext.UserEntities.AsEnumerable().FirstOrDefault(u => u.UserName.Equals(Login));
                    if (ue != null)
                    {
                        ue.TripEntities = new HashSet<TripEntity>(localTravelAppMSSQLDBContext.TripEntities.
                                          Where(t => t.UserEntityId == ue.Id));
                    }
                    return ue;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public UserEntity UpdateUserData(UserEntity user)
        {
            try
            {
                using (LocalTravelAppMSSQLDBContext localTravelAppMSSQLDBContext = new LocalTravelAppMSSQLDBContext())
                {
                    localTravelAppMSSQLDBContext.UserEntities.AddOrUpdate(user);
                    localTravelAppMSSQLDBContext.SaveChanges();
                    return user;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}