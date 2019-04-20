using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Models.TeleportWebApiModels
{
    public class TeleportSearchedCityDistrictCategoriesModel
    {
       public string CategoryType { get; set; }
       public IEnumerable<TeleportSearchedCityDistrictCategoryModel> TeleportSearchedCityDistrictCategoryModels; 
    }
}
