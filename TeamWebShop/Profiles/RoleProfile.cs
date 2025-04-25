using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TeamWebShop.Models.DTOs.Roles;

namespace TeamWebShop.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleDTO>().ReverseMap();
        }
    }
}
