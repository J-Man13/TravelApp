using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using TravelApp.Models.TeleportWebApiModels;

namespace TravelApp.Services
{
    public class TeleportDestination_sCategoriesDetailsImagesServiceWebApi : ITeleportDestination_sCategoriesScoresImagesService
    {
        public IEnumerable<TeleportSearchedCityDistrictCategoriesModel> GetTeleportSearchedCityDistrictCategoryModels(string urbanAreaDetailsLink)
        {
            LinkedList<TeleportSearchedCityDistrictCategoriesModel> teleportSearchedCityDistrictCategoriesModels = new LinkedList<TeleportSearchedCityDistrictCategoriesModel>();

            try
            {

                JArray jArrayCategoriesTypes = GetJObject(urbanAreaDetailsLink)["categories"] as JArray;

                foreach (JObject CategoryTypesJobject in jArrayCategoriesTypes)
                {
                    TeleportSearchedCityDistrictCategoriesModel teleportSearchedCityDistrictCategoriesModel = new TeleportSearchedCityDistrictCategoriesModel();
                    teleportSearchedCityDistrictCategoriesModel.CategoryType = CategoryTypesJobject["label"].Value<string>();

                    LinkedList<TeleportSearchedCityDistrictCategoryModel> teleportSearchedCityDistrictCategoryModels = new LinkedList<TeleportSearchedCityDistrictCategoryModel>();

                    foreach (JObject jCategory in CategoryTypesJobject["data"] as JArray)
                    {
                        TeleportSearchedCityDistrictCategoryModel teleportSearchedCityDistrictCategoryModel = new TeleportSearchedCityDistrictCategoryModel();
                        teleportSearchedCityDistrictCategoryModel.Label = jCategory["label"].Value<String>();
                        teleportSearchedCityDistrictCategoryModel.Value = jCategory[jCategory["type"].Value<String>() + "_value"].Value<String>();
                        teleportSearchedCityDistrictCategoryModels.AddLast(teleportSearchedCityDistrictCategoryModel);
                    }

                    teleportSearchedCityDistrictCategoriesModel.TeleportSearchedCityDistrictCategoryModels = teleportSearchedCityDistrictCategoryModels;
                    teleportSearchedCityDistrictCategoriesModels.AddLast(teleportSearchedCityDistrictCategoriesModel);
                }
            }
            catch(Exception )
            {
                return null;
            }

            return teleportSearchedCityDistrictCategoriesModels;
        }

        public TeleportSearchedCityDistrictScoresInfo GetSearchedCityDistrictScoresInfo(string urbanAreaScoresLink)
        {
            TeleportSearchedCityDistrictScoresInfo teleportSearchedCityDistrictScoresInfo = new TeleportSearchedCityDistrictScoresInfo();

            try
            {
                JObject jObject = GetJObject(urbanAreaScoresLink);

                String summary = jObject["summary"].Value<string>().Replace("<p>","").Replace("</p>","").Replace("<i>", "").Replace("</i>", "")
                .Replace("</b>","").Replace("\n","").Replace("<b>","").Replace("  ","").Replace(".", " . ").Replace("<br>", "").Replace("</br>", "");
                teleportSearchedCityDistrictScoresInfo.Summary = summary;
                teleportSearchedCityDistrictScoresInfo.CityScore = jObject["teleport_city_score"].Value<int>();

                LinkedList<TeleportSearchedCityDistrictScore> teleportSearchedCityDistrictScores = new LinkedList<TeleportSearchedCityDistrictScore>();

                foreach (JObject category in jObject["categories"] as JArray)
                {
                    TeleportSearchedCityDistrictScore teleportSearchedCityDistrictScore = new TeleportSearchedCityDistrictScore();
                    teleportSearchedCityDistrictScore.Color = category["color"].Value<String>();
                    teleportSearchedCityDistrictScore.Name = category["name"].Value<String>();
                    teleportSearchedCityDistrictScore.Score_out_of_10 = category["score_out_of_10"].Value<int>();
                    teleportSearchedCityDistrictScores.AddLast(teleportSearchedCityDistrictScore);
                }

                teleportSearchedCityDistrictScoresInfo.TeleportSearchedCityDistrictScores = teleportSearchedCityDistrictScores;
            }
            catch (Exception)
            {
                return null;
            }
            return teleportSearchedCityDistrictScoresInfo;
        }

        public BitmapImage GetSearchedCityImage(string urbanAreaImagesLink)
        {
            try
            {
                string imageUrl = (((((GetJObject(urbanAreaImagesLink))["photos"] as JArray)[0] as JObject)["image"]) as JObject)["mobile"].Value<String>();
                var buffer = new WebClient().DownloadData(imageUrl);
                var image = new BitmapImage();
                using (var stream = new MemoryStream(buffer))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                }
                image.Freeze();
                return image;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private JObject GetJObject(string url)
        {
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            string jsonString = webClient.DownloadString(url);
            return JsonConvert.DeserializeObject(jsonString) as JObject;
        }

    }

}
