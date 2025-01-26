using AutoMapper;
using CarRental.Dto.Reviews;
using CarRental.Model.Entities;

namespace CarRental.Api.Configurations.Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewDto>()
                .ForMember(x => x.CarName, d => d.MapFrom(s => $"{s.Car.Brand} {s.Car.Model} {s.Car.Year} {s.Car.Color}"))
                .ForMember(x => x.UserName, d => d.MapFrom(s => $"{s.User.FirstName} {s.User.LastName}"));
        }
    }
}
