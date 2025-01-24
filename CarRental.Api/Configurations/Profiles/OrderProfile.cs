using AutoMapper;
using CarRental.Dto.Orders;
using CarRental.Model.Entities;

namespace CarRental.Api.Configurations.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<Order, OrderDto>()
                .ForMember(x => x.ProductCount, d => d.MapFrom(s => s.Products == null ? 0 : s.Products.Count));
        }
    }
}
