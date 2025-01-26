using AutoMapper;
using CarRental.Dto.Users;
using CarRental.Model.Entities;

namespace CarRental.Api.Configurations.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.RentalCount, d => d.MapFrom(s => s.Rentals == null ? 0 : s.Rentals.Count))
                .ForMember(x => x.ReviewCount, d => d.MapFrom(s => s.Reviews == null ? 0 : s.Reviews.Count));
        }
    }
}
