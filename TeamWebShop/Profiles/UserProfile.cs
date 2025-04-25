using AutoMapper;
using TeamWebShop.Data;
using TeamWebShop.Models.DTOs.Users;

namespace TeamWebShop.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ShopUser, UserDTO>().ReverseMap();
        }
    }
}
