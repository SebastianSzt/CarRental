using AutoMapper;
using CarRental.Dto.Cars;
using CarRental.Model.Entities;
using CarRental.Repository.Cars;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CarsController(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var car = await _carRepository.GetCarByIdAsync(id);
            if (car == null)
                return NotFound();

            var carDto = _mapper.Map<CarDto>(car);

            return Ok(carDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cars = await _carRepository.GetAllCarsAsync();
            if (cars == null)
                return NotFound();

            var carsDto = _mapper.Map<List<CarDto>>(cars);

            return Ok(carsDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarInputDto car)
        {
            if (car == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newCar = new Car()
            {
                Brand = car.Brand,
                Model = car.Model,
                Color = car.Color,
                Year = car.Year,
                FuelType = car.FuelType,
                FuelConsumption = car.FuelConsumption,
                FuelTankCapacity = car.FuelTankCapacity,
                HorsePower = car.HorsePower,
                SeatCount = car.SeatCount,
                PricePerDay = car.PricePerDay,
                Location = car.Location,
                Image = car.Image,
                Description = car.Description
            };

            var result = await _carRepository.SaveCarAsync(newCar);
            if (!result)
                throw new Exception("Error saving car");

            var carDto = _mapper.Map<CarDto>(newCar);

            return Ok(carDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CarInputDto car)
        {
            if (car == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCar = await _carRepository.GetCarByIdAsync(id);
            if (existingCar == null)
                return NotFound();

            existingCar.Brand = car.Brand;
            existingCar.Model = car.Model;
            existingCar.Color = car.Color;
            existingCar.Year = car.Year;
            existingCar.FuelType = car.FuelType;
            existingCar.FuelConsumption = car.FuelConsumption;
            existingCar.FuelTankCapacity = car.FuelTankCapacity;
            existingCar.HorsePower = car.HorsePower;
            existingCar.SeatCount = car.SeatCount;
            existingCar.PricePerDay = car.PricePerDay;
            existingCar.Location = car.Location;
            existingCar.Image = car.Image;
            existingCar.Description = car.Description;

            var result = await _carRepository.SaveCarAsync(existingCar);
            if (!result)
                throw new Exception("Error updating car");

            var carDto = _mapper.Map<CarDto>(existingCar);

            return Ok(carDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingCar = await _carRepository.GetCarByIdAsync(id);
            if (existingCar == null)
                return NotFound();

            var result = await _carRepository.DeleteCarAsync(id);
            if (!result)
                throw new Exception("Error deleting car");

            return Ok();
        }
    }
}
