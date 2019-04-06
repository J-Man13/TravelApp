using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelApp.Models.EntityModels
{
    public class UserEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set;}
        public string UserName { get; set;}
        public string UserLoginImagePath { get; set; }
        public string UserEmail { get; set;}
        public string PasswordHash { get; set;}

        public ICollection<TripEntity> TripEntities { get; set;}
    }
}
