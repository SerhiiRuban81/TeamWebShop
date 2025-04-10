using AutoMapper;
using TeamWebShop.Models.DTOs.Products;

namespace TeamWebShop.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductProfile, ProductDTO>()
                .ReverseMap();
        }
    }
}
