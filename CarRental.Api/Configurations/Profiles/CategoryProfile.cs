using AutoMapper;
using CarRental.Dto.Categories;
using CarRental.Model.Entities;

namespace CarRental.Api.Configurations.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(x => x.ProductCount, d => d.MapFrom(s => s.Products == null ? 0 : s.Products.Count));
        }
    }
}
