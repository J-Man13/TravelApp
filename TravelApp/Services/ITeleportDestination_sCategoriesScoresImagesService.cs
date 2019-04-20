using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TravelApp.Models.TeleportWebApiModels;

namespace TravelApp.Services
{
    interface ITeleportDestination_sCategoriesScoresImagesService
    {
        IEnumerable<TeleportSearchedCityDistrictCategoriesModel> GetTeleportSearchedCityDistrictCategoryModels(string urbanAreaDetailsLink);
        TeleportSearchedCityDistrictScoresInfo GetSearchedCityDistrictScoresInfo(string urbanAreaScoresLink);
        BitmapImage GetSearchedCityImage(string urbanAreaImagesLink);
    }
}
