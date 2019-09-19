using StatePrinting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models.OpenWeatherWebApiModels
{
    public class WeatherModel
    {
        public DateTime Date { get; set; }
        public int MinTemp { get; set; }
        public int MaxTemp { get; set; }
        public double Pressure { get; set; }
        public double WindSpeed { get; set; }
        public int Humidity { get; set; }
        public string Status { get; set; }
        public string IconLink { get; set; }

        // Nice stuff ahead!
        static readonly Stateprinter printer = new Stateprinter();
        public override string ToString()
        {
            return printer.PrintObject(this);
        }
    }
}
