using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Model.Entities;

namespace CarRental.Model.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Details).HasMaxLength(2000);
            builder.Property(x => x.TotalCost).HasColumnType("decimal(18,2)");

            builder.HasMany(x => x.Products)
                .WithMany(x => x.Orders);
        }
    }
}
