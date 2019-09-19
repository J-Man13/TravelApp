using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Models.OpenWeatherWebApiModels;

namespace TravelApp.Services
{
    interface IWeatherModelsService
    {
        IEnumerable<WeatherModel> GetWeatherModels(string city);
    }
}
