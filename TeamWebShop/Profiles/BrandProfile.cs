using AutoMapper;
using ShopLibrary;
using TeamWebShop.Models.DTOs.Brands;

namespace TeamWebShop.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandDTO>().ReverseMap();
        }
    }
}
