using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Helpers;
using TravelApp.Models.OpenWeatherWebApiModels;
using TravelApp.Services;

namespace TravelApp.ViewModels.AppMainViewModelViewModels
{
    public class TripDescriptionViewModel : ViewModelBase
    {
        private IWeatherModelsService weatherModelsService;
        private WeatherModel currentWeatherModel;



        public TripDescriptionViewModel()
        {
            weatherModelsService = new WeatherModelsWebService();
            DestinationtPointFullName = CurrentUserEntity.SelectedTripEntity.ToSearchedCityDistrictModel.FullName;
            System.Diagnostics.Debug.WriteLine(CurrentUserEntity.SelectedTripEntity.ToSearchedCityDistrictModel.Name);
            System.Diagnostics.Debug.WriteLine(CurrentUserEntity.SelectedTripEntity.ToSearchedCityDistrictModel.Name);
            System.Diagnostics.Debug.WriteLine(CurrentUserEntity.SelectedTripEntity.ToSearchedCityDistrictModel.Name);
            System.Diagnostics.Debug.WriteLine(CurrentUserEntity.SelectedTripEntity.ToSearchedCityDistrictModel.Name);
            currentWeatherModel = weatherModelsService.GetWeatherModels(CurrentUserEntity.SelectedTripEntity.ToSearchedCityDistrictModel.Name).FirstOrDefault();
            weatherModelsService = new WeatherModelsWebService();
            WeatherIconUri = currentWeatherModel.IconLink;

        }

        public string DestinationtPointFullName { get { return destinationtPointFullName; } set { Set(ref destinationtPointFullName, value); } }
        private string destinationtPointFullName;

        public string DestinationtPointTemperature { get { return "Temperature : "+ (currentWeatherModel.MaxTemp+currentWeatherModel.MinTemp)/2 + "°C"; } set { Set(ref destinationtPointTemperature, value); } }
        private string destinationtPointTemperature;

        public string DestinationtPointPressure { get { return "Pressure : " + currentWeatherModel.Pressure +"hPa"; } set { Set(ref destinationtPointPressure, value); } }
        private string destinationtPointPressure;

        
        public string DestinationtPointWindSpeed { get { return "Wind speed : "+currentWeatherModel.WindSpeed + "m/s"; } set { Set(ref destinationtPointWindSpeed, value); } }
        private string destinationtPointWindSpeed;

        public string DestinationtPointHumidity { get { return "Humidity : " + currentWeatherModel.Humidity + "%"; } set { Set(ref destinationtPointHumidity, value); } }
        private string destinationtPointHumidity;

        public string WeatherIconUri { get { return weatherIconUri; } set { Set(ref weatherIconUri, value); } }
        private string weatherIconUri;


        
    }
}
