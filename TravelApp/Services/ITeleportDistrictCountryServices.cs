using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Models.TeleportWebApiModels;

namespace TravelApp.Services
{
    interface ITeleportDistrictCountryServices
    {
        IEnumerable<TeleportSearchedCityDistrictModel> GetSearchedCityDistrictModel(string city);
        TeleportCountryInfo GetTeleportCountryInfo(string countryLink);
        IEnumerable<TeleportCountrySalary> GetTeleportCountrySalaries(string countryLink);
    }
}
