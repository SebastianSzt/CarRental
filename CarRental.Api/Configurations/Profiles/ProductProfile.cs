using AutoMapper;
using CarRental.Dto.Products;
using CarRental.Model.Entities;

namespace CarRental.Api.Configurations.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(x => x.CategoryName, d => d.MapFrom(s => s.Category.Name))
                .ForMember(x => x.OrderCount, d => d.MapFrom(s => s.Orders == null ? 0 : s.Orders.Count));
        }
    }
}
