using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models.TeleportWebApiModels
{
    public class TeleportCountryInfo
    {
        public String Name { get; set; }
        public String CurrencyCode { get; set;}
        public String Iso_alpha2 { get; set; }
        public String Iso_alpha3 { get; set; }
        public int Population { get; set; }

        public IEnumerable<TeleportCountrySalary> TeleportCountrySalaries { get; set; }
    }
}
