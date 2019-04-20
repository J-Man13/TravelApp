using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TravelApp.Models.EntityModels;

namespace TravelApp.Services
{
    public class TripEntitiesLocalMSSQLDB : ITripEntities
    {
        public TripEntity AddTripEntity(TripEntity tripEntity)
        {
            try
            {
                using (LocalTravelAppMSSQLDBContext localTravelAppMSSQLDBContext = new LocalTravelAppMSSQLDBContext())
                {
                    TripEntity te = localTravelAppMSSQLDBContext.TripEntities.Add(tripEntity);
                    localTravelAppMSSQLDBContext.SaveChanges();
                    return te;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }

        public TripEntity DeleteTripEntity(TripEntity tripEntity)
        {
            try
            {
                using (LocalTravelAppMSSQLDBContext localTravelAppMSSQLDBContext = new LocalTravelAppMSSQLDBContext())
                {
                    TripEntity te = localTravelAppMSSQLDBContext.TripEntities.Remove(tripEntity);
                    localTravelAppMSSQLDBContext.SaveChanges();
                    return te;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }

        public TripEntity UpdateTripEntity(TripEntity tripEntity)
        {
            try
            {
                using (LocalTravelAppMSSQLDBContext localTravelAppMSSQLDBContext = new LocalTravelAppMSSQLDBContext())
                {
                    TripEntity te = localTravelAppMSSQLDBContext.TripEntities.Find(tripEntity.Id);
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