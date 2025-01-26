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
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.StartDate).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.EndDate).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.TotalPrice).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Status).HasMaxLength(50);
            builder.Property(x => x.CarId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();

            builder.HasOne(x => x.Car)
                .WithMany(x => x.Rentals)
                .HasForeignKey(x => x.CarId);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Rentals)
                .HasForeignKey(x => x.UserId);
        }
    }
}
