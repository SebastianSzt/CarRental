using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Model.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Brand).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Model).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Color).HasMaxLength(50);
            builder.Property(x => x.Year).IsRequired();
            builder.Property(x => x.FuelType).HasMaxLength(50);
            builder.Property(x => x.FuelConsumption).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.PricePerDay).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.Location).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Image).HasMaxLength(500);
            builder.Property(x => x.Description).HasMaxLength(1000);

            builder.HasMany(x => x.Rentals)
                .WithOne(x => x.Car);

            builder.HasMany(x => x.Reviews)
                .WithOne(x => x.Car);
        }
    }
}
