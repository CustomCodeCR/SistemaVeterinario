// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class ViewProductCategoryMappingConfiguration : IEntityTypeConfiguration<Viewproductcategorymapping>
{
    public void Configure(EntityTypeBuilder<Viewproductcategorymapping> builder)
    {
        builder
                .HasNoKey()
                .ToView("VIEWPRODUCTCATEGORYMAPPING");

        builder.Property(e => e.Categoryid)
            .HasColumnType("NUMBER")
            .HasColumnName("CATEGORYID");
        builder.Property(e => e.Categoryname)
            .HasMaxLength(50)
            .HasColumnName("CATEGORYNAME");
        builder.Property(e => e.Productid)
            .HasColumnType("NUMBER")
            .HasColumnName("PRODUCTID");
        builder.Property(e => e.Productname)
            .HasMaxLength(50)
            .HasColumnName("PRODUCTNAME");
    }
}