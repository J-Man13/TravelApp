using System;
using System.Linq;
using TravelApp.Helpers;
using TravelApp.Models.EntityModels;

namespace TravelApp.Services
{
    public class TripEntitiesLocalMSSQLDB : ITripEntitiesService
    {
        public bool AddTripEntity(TripEntity tripEntity)
        {
            try
            {
                using (LocalTravelAppMSSQLDBContext localTravelAppMSSQLDBContext = new LocalTravelAppMSSQLDBContext())
                {
                    CurrentUserEntity.UserEntity.TripEntities.Add(tripEntity);                    
                    localTravelAppMSSQLDBContext.TripEntities.Add(tripEntity);
                    localTravelAppMSSQLDBContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteTripEntity(TripEntity tripEntity)
        {
            try
            {
                using (LocalTravelAppMSSQLDBContext localTravelAppMSSQLDBContext = new LocalTravelAppMSSQLDBContext())
                {
                    bool removedPerUser = CurrentUserEntity.UserEntity.TripEntities.ToList().
                    Remove(CurrentUserEntity.UserEntity.TripEntities.ToList().Where(t => t.Id == tripEntity.Id).FirstOrDefault());

                    bool removedFromTrips = localTravelAppMSSQLDBContext.TripEntities.ToList().
                    Remove(localTravelAppMSSQLDBContext.TripEntities.ToList().Where(t => t.Id == tripEntity.Id).FirstOrDefault());

                    localTravelAppMSSQLDBContext.SaveChanges();

                    if (removedPerUser && removedFromTrips)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteTripEntity(long id)
        {
            try
            {
                using (LocalTravelAppMSSQLDBContext localTravelAppMSSQLDBContext = new LocalTravelAppMSSQLDBContext())
                {
                    bool removedPerUser = CurrentUserEntity.UserEntity.TripEntities.
                    Remove(CurrentUserEntity.UserEntity.TripEntities.ToList().Where(t => t.Id == id).FirstOrDefault());

                    TripEntity removedFromTrips = localTravelAppMSSQLDBContext.TripEntities.Remove(localTravelAppMSSQLDBContext.TripEntities.ToList().Where(t => t.Id == id).FirstOrDefault());

                    localTravelAppMSSQLDBContext.SaveChanges();

                    if (removedPerUser && removedFromTrips != null)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public TripEntity UpdateTripEntity(TripEntity tripEntity)
        {
            throw new NotImplementedException();
        }
    }
}