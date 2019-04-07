using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using System.Windows;
using TravelApp.Models.TeleportWebApiModels;

namespace TravelApp.Services
{
    public class TeleportDistrictCountryWebService : ITeleportDistrictCountryServices
    {
        private readonly string TELEPORT_API_URL;

        public TeleportDistrictCountryWebService()
        {
            TELEPORT_API_URL = ConfigurationSettings.AppSettings["TeleportCitySearchWebApiURLString"];
        }

        public IEnumerable<TeleportSearchedCityDistrictModel> GetSearchedCityDistrictModel(string city)
        {
            LinkedList<TeleportSearchedCityDistrictModel> searchedCityDistrictModelsLinkedList = new LinkedList<TeleportSearchedCityDistrictModel>();
            try
            {
                JObject jObject = GetJObject(TELEPORT_API_URL+city);
                foreach (JObject j in (jObject["_embedded"])["city:search-results"] as JArray) {

                    string infoGeonameIdLink = ((j["_links"])["city:item"])["href"].Value<String>();
                    string fullName = j["matching_full_name"].Value<String>();

                    JObject jObjectGeonameId = GetJObject(infoGeonameIdLink);
                    double lattitude = ((jObjectGeonameId["location"])["latlon"])["latitude"].Value<double>();
                    double longtitude = ((jObjectGeonameId["location"])["latlon"])["longitude"].Value<double>();

                    string countryLink = ((jObjectGeonameId["_links"])["city:country"])["href"].Value<string>();
                    JObject jObjectCountry = GetJObject(countryLink);
                    string countrySalariesLink = ((jObjectCountry["_links"])["country:salaries"])["href"].Value<string>();

                    string urbanAreaLink = "";
                    string urbanAreaSalariesLink = "";
                    string urbanAreaDetailsLink = "";
                    string urbanAreaScoresLink = "";
                    string urbanAreaImagesLink = "";

                    if ((jObjectGeonameId["_links"])["city:urban_area"] != null)
                    {
                        urbanAreaLink = ((jObjectGeonameId["_links"])["city:urban_area"])["href"].Value<string>();
                        JObject jObjectUrbanArea = GetJObject(urbanAreaLink);
                        urbanAreaSalariesLink = ((jObjectUrbanArea["_links"])["ua:salaries"])["href"].Value<string>();
                        urbanAreaDetailsLink = ((jObjectUrbanArea["_links"])["ua:details"])["href"].Value<string>();
                        urbanAreaScoresLink = ((jObjectUrbanArea["_links"])["ua:scores"])["href"].Value<string>();
                        urbanAreaImagesLink = ((jObjectUrbanArea["_links"])["ua:images"])["href"].Value<string>();
                    }
                      
                    searchedCityDistrictModelsLinkedList.AddLast(new TeleportSearchedCityDistrictModel()
                    {
                        InfoGeonameIdLink = infoGeonameIdLink,
                        Lattitude = lattitude,
                        FullName = fullName,
                        Longtitude = longtitude,
                        CountryLink = countryLink,
                        CountrySalariesLink = countrySalariesLink,
                        UrbanAreaLink = urbanAreaLink,
                        UrbanAreaSalariesLink = urbanAreaSalariesLink,
                        UrbanAreaDetailsLink = urbanAreaDetailsLink,
                        UrbanAreaScoresLink = urbanAreaScoresLink,
                        UrbanAreaImagesLink = urbanAreaImagesLink
                    });
                }
            }
            catch (Exception E)
            {
                MessageBox.Show("GetSearchedCityDistrictModel : " + E.ToString());
                return null;
            }
            return searchedCityDistrictModelsLinkedList;
        }

        public TeleportCountryInfo GetTeleportCountryInfo(string countryLink)
        {
            TeleportCountryInfo teleportCountryInfo = new TeleportCountryInfo();

            try
            {
                JObject jObjectCountry = GetJObject(countryLink);
                teleportCountryInfo.Name = jObjectCountry["name"].Value<string>();
                teleportCountryInfo.CurrencyCode = jObjectCountry["currency_code"].Value<string>();
                teleportCountryInfo.Iso_alpha2 = jObjectCountry["iso_alpha2"].Value<string>();
                teleportCountryInfo.Iso_alpha3 = jObjectCountry["iso_alpha3"].Value<string>();
                teleportCountryInfo.Population = jObjectCountry["population"].Value<int>();
                teleportCountryInfo.TeleportCountrySalaries = GetTeleportCountrySalaries(((jObjectCountry["_links"])["country:salaries"])["href"].Value<string>());

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
            return teleportCountryInfo;
        }

        public IEnumerable<TeleportCountrySalary> GetTeleportCountrySalaries(string countrySalariesLink)
        {
            LinkedList<TeleportCountrySalary> teleportCountrySalaries = new LinkedList<TeleportCountrySalary>();
            try
            {
                JObject jObjectCountrySalaries = GetJObject(countrySalariesLink);
                JArray jArraySalaries = jObjectCountrySalaries["salaries"] as JArray;
                foreach (JObject j in jArraySalaries)
                {
                    TeleportCountrySalary teleportCountrySalary = new TeleportCountrySalary();
                    teleportCountrySalary.SpecialtyName = (j["job"])["title"].Value<string>();
                    teleportCountrySalary.SalaryPerMonth = (int)((j["salary_percentiles"])["percentile_75"].Value<double>() / 12);
                    teleportCountrySalaries.AddLast(teleportCountrySalary);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
            return teleportCountrySalaries;
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