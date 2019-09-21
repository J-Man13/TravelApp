using StatePrinting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models.TeleportWebApiModels
{
    public class TeleportSearchedCityDistrictModel
    {
        public string InfoGeonameIdLink { get;set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public double Lattitude { get; set; }
        public double Longtitude { get; set; }

        public string CountryLink { get; set; }

        public string CountrySalariesLink {get; set;}

        public string UrbanAreaLink { get; set; }

        public string UrbanAreaSalariesLink { get; set; }
        public string UrbanAreaDetailsLink { get; set; }
        public string UrbanAreaScoresLink { get; set; }
        public string UrbanAreaImagesLink { get; set; }

        // Nice stuff ahead!
        static readonly Stateprinter printer = new Stateprinter();
        public override string ToString()
        {
            return printer.PrintObject(this);
        }
    }
}