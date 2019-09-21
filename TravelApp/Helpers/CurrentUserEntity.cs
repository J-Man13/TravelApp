using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Models.EntityModels;

namespace TravelApp.Helpers
{
    public class CurrentUserEntity
    {
        private static UserEntity userEntity;
        public static UserEntity UserEntity
        {
            get {return userEntity;}
            set { userEntity = value; }
        }
        private static TripEntity selectedTripEntity;
        public static TripEntity SelectedTripEntity
        {
            get { return selectedTripEntity; }
            set { selectedTripEntity = value; }
        }

    }
}
