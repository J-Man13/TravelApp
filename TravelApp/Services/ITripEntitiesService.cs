using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Models.EntityModels;

namespace TravelApp.Services
{
    interface ITripEntitiesService
    {
        bool AddTripEntity(TripEntity tripEntity);
        TripEntity UpdateTripEntity(TripEntity tripEntity);
        bool DeleteTripEntity(TripEntity tripEntity);
        bool DeleteTripEntity(long id);
    }
}