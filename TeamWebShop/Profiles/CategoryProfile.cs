using AutoMapper;
using ShopLibrary;
using TeamWebShop.Models.DTOs.Categories;

namespace TeamWebShop.Profiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
