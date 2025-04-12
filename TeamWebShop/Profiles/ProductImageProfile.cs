using AutoMapper;
using TeamWebShop.Models.DTOs.ProductImages;

namespace TeamWebShop.Profiles
{
    public class ProductImageProfile : Profile
    {
        public ProductImageProfile()
        {
            CreateMap<ProductImageProfile, ProductImageDTO>()
                .ReverseMap();
        }



    }
}
