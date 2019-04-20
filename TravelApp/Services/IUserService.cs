using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Models.EntityModels;

namespace TravelApp.Services
{
    public interface IUserService
    {
        UserEntity RegistrateUser(UserEntity userEntity);
        UserEntity FindUserByLogin(string Login);
        UserEntity UpdateUserData(UserEntity User);
    }
}
