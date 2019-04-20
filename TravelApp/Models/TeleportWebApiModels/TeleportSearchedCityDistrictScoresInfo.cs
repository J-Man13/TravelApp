using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models.TeleportWebApiModels
{
    public class TeleportSearchedCityDistrictScoresInfo
    {
        public string Summary { get; set; }
        public int CityScore { get; set; }
        public IEnumerable<TeleportSearchedCityDistrictScore> TeleportSearchedCityDistrictScores { get; set; }
    }
}
