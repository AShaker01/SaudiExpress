using AutoMapper;
using SaudiExpress.API.ViewModels;
using SaudiExpress.Business.Models.User;
namespace SaudiExpress.API.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserDTO, UserViewModel>()
                .ForMember(des => des.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.FullName, opt => opt.MapFrom(src => $"{src.FirstName} - {src.LastName}"));
        }
    }
}
