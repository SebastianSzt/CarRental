using CarRental.Dto.Cars;

namespace CarRental.Web.Services
{
    public interface ICarService
    {
        Task<IEnumerable<CarDto>> GetCarsAsync();
    }
}
