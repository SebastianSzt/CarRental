using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Model.Entities;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Model
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            if (!context.Cars.Any())
            {
                context.Cars.AddRange(
                    new Car
                    {
                        Brand = "Toyota",
                        Model = "Corolla",
                        Color = "Czarny",
                        Year = 2020,
                        FuelType = "Benzyna",
                        FuelConsumption = 6.5m,
                        PricePerDay = 150,
                        Location = "Warszawa",
                        Description = "Idealny do miasta",
                        Image = "toyota-corolla.jpg"
                    },
                    new Car
                    {
                        Brand = "Ford",
                        Model = "Focus",
                        Color = "Niebieski",
                        Year = 2018,
                        FuelType = "Diesel",
                        FuelConsumption = 5.5m,
                        PricePerDay = 120,
                        Location = "Kraków",
                        Description = "Wygodny i ekonomiczny",
                        Image = "ford-focus.jpg"
                    }
                );
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var passwordHasher = new PasswordHasher<User>();

                var adminUser = new User
                {
                    Email = "admin@gmail.com",
                    FirstName = "Admin",
                    LastName = "Administrator",
                    Role = "Admin",
                    PhoneNumber = "123456789"
                };

                adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "zaq1@WSX");

                context.Users.Add(adminUser);
                context.SaveChanges();

                var regularUser = new User
                {
                    Email = "kamilkowal@gmail.com",
                    FirstName = "Kamil",
                    LastName = "Kowal",
                    Role = "User",
                    PhoneNumber = "987654321"
                };

                regularUser.PasswordHash = passwordHasher.HashPassword(regularUser, "zaq1@WSX");
                context.Users.Add(regularUser);
            }

            if (!context.Rentals.Any())
            {
                var car = context.Cars.FirstOrDefault();
                var user = context.Users.FirstOrDefault(u => u.Role == "User");

                if (car != null && user != null)
                {
                    context.Rentals.Add(new Rental
                    {
                        StartDate = DateTime.Now.AddDays(-2),
                        EndDate = DateTime.Now,
                        TotalPrice = car.PricePerDay * 2,
                        Status = "Completed",
                        CarId = car.Id,
                        UserId = user.Id
                    });
                    context.SaveChanges();
                }
            }

            if (!context.Reviews.Any())
            {
                var car = context.Cars.FirstOrDefault();
                var user = context.Users.FirstOrDefault(u => u.Role == "User");

                if (car != null && user != null)
                {
                    context.Reviews.Add(new Review
                    {
                        Comment = "Bardzo wygodny samochód, idealny na krótkie trasy.",
                        Rating = 5,
                        Date = DateTime.Now,
                        CarId = car.Id,
                        UserId = user.Id
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
