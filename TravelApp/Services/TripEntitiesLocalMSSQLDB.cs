using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                MessageBox.Show(e.ToString());
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
                MessageBox.Show(e.ToString());
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
                MessageBox.Show(e.ToString());
                return false;
            }
        }

        public TripEntity UpdateTripEntity(TripEntity tripEntity)
        {
            try
            {
                using (LocalTravelAppMSSQLDBContext localTravelAppMSSQLDBContext = new LocalTravelAppMSSQLDBContext())
                {
                    TripEntity te = CurrentUserEntity.UserEntity.TripEntities.FirstOrDefault(t => t.Id == tripEntity.Id);
                    if (te != null)
                    {
                        localTravelAppMSSQLDBContext.Entry(te).CurrentValues.SetValues(tripEntity);
                        localTravelAppMSSQLDBContext.SaveChanges();
                    }
                    return te;
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