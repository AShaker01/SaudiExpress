using AutoMapper;
using SaudiExpress.Business.Models.User;
using SaudiExpress.Database.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiExpress.Business.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<AppUser, UserDTO>();
        }
    }
}
