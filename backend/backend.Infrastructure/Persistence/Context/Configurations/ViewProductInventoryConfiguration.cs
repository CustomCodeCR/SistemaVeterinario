// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class ViewProductInventoryConfiguration : IEntityTypeConfiguration<Viewproductinventory>
{
    public void Configure(EntityTypeBuilder<Viewproductinventory> builder)
    {
        builder
                .HasNoKey()
                .ToView("VIEWPRODUCTINVENTORY");

        builder.Property(e => e.Description)
            .HasMaxLength(100)
            .HasColumnName("DESCRIPTION");
        builder.Property(e => e.Price)
            .HasColumnType("NUMBER(10,2)")
            .HasColumnName("PRICE");
        builder.Property(e => e.Productid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("PRODUCTID");
        builder.Property(e => e.Productname)
            .HasMaxLength(50)
            .HasColumnName("PRODUCTNAME");
        builder.Property(e => e.Quantity)
            .HasColumnType("NUMBER")
            .HasColumnName("QUANTITY");
        builder.Property(e => e.Updatedate)
            .HasColumnType("DATE")
            .HasColumnName("UPDATEDATE");
    }
}