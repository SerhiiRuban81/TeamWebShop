using AutoMapper;
using ShopLibrary;
using TeamWebShop.Models.DTOs.Products;

namespace TeamWebShop.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
