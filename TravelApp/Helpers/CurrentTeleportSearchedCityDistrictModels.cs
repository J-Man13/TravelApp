using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Models.EntityModels;
using TravelApp.Models.TeleportWebApiModels;

namespace TravelApp.Helpers
{
    public class CurrentTeleportSearchedCityDistrictModels
    {
        private static TeleportSearchedCityDistrictModel startPointTeleportSearchedCityDistrictModel;
        public static TeleportSearchedCityDistrictModel StartPointTeleportSearchedCityDistrictModel
        {
            get { return startPointTeleportSearchedCityDistrictModel; }
            set { startPointTeleportSearchedCityDistrictModel = value; }
        }

        private static TeleportSearchedCityDistrictModel destinationPointTeleportSearchedCityDistrictModel;
        public static TeleportSearchedCityDistrictModel DestinationPointTeleportSearchedCityDistrictModel
        {
            get { return destinationPointTeleportSearchedCityDistrictModel; }
            set { destinationPointTeleportSearchedCityDistrictModel = value; }
        }
    }
}
