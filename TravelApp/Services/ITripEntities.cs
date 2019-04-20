using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Models.EntityModels;

namespace TravelApp.Services
{
    interface ITripEntities
    {
        TripEntity AddTripEntity(TripEntity tripEntity);
        TripEntity UpdateTripEntity(TripEntity tripEntity);
        TripEntity DeleteTripEntity(TripEntity tripEntity);
    }
}