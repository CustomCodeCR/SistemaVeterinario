// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<Productcategory>
{
    public void Configure(EntityTypeBuilder<Productcategory> builder)
    {
        builder.HasKey(e => e.Categoryid).HasName("SYS_C008132");

        builder.ToTable("PRODUCTCATEGORY");

        builder.Property(e => e.Categoryid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("CATEGORYID");
        builder.Property(e => e.Auditcreatedate)
            .HasPrecision(7)
            .HasColumnName("AUDITCREATEDATE");
        builder.Property(e => e.Auditcreateuser)
            .HasColumnType("NUMBER")
            .HasColumnName("AUDITCREATEUSER");
        builder.Property(e => e.Auditdeletedate)
            .HasPrecision(7)
            .HasColumnName("AUDITDELETEDATE");
        builder.Property(e => e.Auditdeleteuser)
            .HasColumnType("NUMBER")
            .HasColumnName("AUDITDELETEUSER");
        builder.Property(e => e.Auditupdatedate)
            .HasPrecision(7)
            .HasColumnName("AUDITUPDATEDATE");
        builder.Property(e => e.Auditupdateuser)
            .HasColumnType("NUMBER")
            .HasColumnName("AUDITUPDATEUSER");
        builder.Property(e => e.Categoryname)
            .HasMaxLength(50)
            .HasColumnName("CATEGORYNAME");
        builder.Property(e => e.Description)
            .HasMaxLength(100)
            .HasColumnName("DESCRIPTION");
        builder.Property(e => e.State)
            .HasColumnType("NUMBER")
            .HasColumnName("STATE");
    }
}