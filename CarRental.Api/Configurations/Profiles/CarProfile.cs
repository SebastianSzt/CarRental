using AutoMapper;
using CarRental.Dto.Cars;
using CarRental.Model.Entities;

namespace CarRental.Api.Configurations.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarDto>()
                .ForMember(x => x.RentalCount, d => d.MapFrom(s => s.Rentals == null ? 0 : s.Rentals.Count))
                .ForMember(x => x.ReviewCount, d => d.MapFrom(s => s.Reviews == null ? 0 : s.Reviews.Count))
                .ForMember(x => x.AverageRating, d => d.MapFrom(s => s.Reviews == null || s.Reviews.Count == 0 ? 0 : s.Reviews.Average(r => r.Rating)));
        }
    }
}
