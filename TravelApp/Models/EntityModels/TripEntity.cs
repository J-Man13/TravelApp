using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelApp.Models.TeleportWebApiModels;

namespace TravelApp.Models.EntityModels
{
    public class TripEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public DateTime DepartmentDateTime { get; set; }
        public String TripAim { get; set; }

        public TeleportSearchedCityDistrictModel FromSearchedCityDistrictModel { get; set; }
        public TeleportSearchedCityDistrictModel ToSearchedCityDistrictModel { get; set; }

        public long UserEntityId { get; set; }
        public UserEntity UserEntity { get; set; }
    }
}
