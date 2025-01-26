using AutoMapper;
using CarRental.Dto.Rentals;
using CarRental.Model.Entities;

namespace CarRental.Api.Configurations.Profiles
{
    public class RentalProfile : Profile
    {
        public RentalProfile()
        {
            CreateMap<Rental, RentalDto>()
                .ForMember(x => x.CarName, d => d.MapFrom(s => $"{s.Car.Brand} {s.Car.Model} {s.Car.Year} {s.Car.Color}"))
                .ForMember(x => x.UserName, d => d.MapFrom(s => $"{s.User.FirstName} {s.User.LastName}"));
        }
    }
}
