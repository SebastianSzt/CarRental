using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Model.Entities;

namespace CarRental.Model
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            if (context.Categories.Any())
            {
                return;
            }

            context.Categories.AddRange(
                new Category()
                {
                    Name = "Komputery"
                },
                new Category()
                {
                    Name = "Tablety"
                });

            context.SaveChanges();

            if(context.Products.Any())
            {
                return;
            }

            context.Products.AddRange(
                new Product()
                {
                    Name = "Dell",
                    Description = "Laptop dla studenta",
                    Price = 1000,
                    CategoryId = context.Categories.Where(x => x.Name == "Komputery").First().Id
                },
                new Product()
                {
                    Name = "iPad Pro",
                    Description = "Tablet dla wymagających",
                    Price = 3000,
                    CategoryId = context.Categories.Where(x => x.Name == "Tablety").First().Id
                });

            context.SaveChanges();
        }
    }
}
