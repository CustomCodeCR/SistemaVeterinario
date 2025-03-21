// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class ProductCategoryRelationConfiguration : IEntityTypeConfiguration<Productcategoryrelation>
{
    public void Configure(EntityTypeBuilder<Productcategoryrelation> builder)
    {
        builder.HasKey(e => new { e.Productid, e.Categoryid });

        builder.ToTable("PRODUCTCATEGORYRELATION");

        builder.HasIndex(e => e.Categoryid, "IDX_PRODUCTCATEGORYRELATION_CATEGORYID");

        builder.HasIndex(e => e.Productid, "IDX_PRODUCTCATEGORYRELATION_PRODUCTID");

        builder.Property(e => e.Productid)
            .HasColumnType("NUMBER")
            .HasColumnName("PRODUCTID");
        builder.Property(e => e.Categoryid)
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
        builder.Property(e => e.State)
            .HasColumnType("NUMBER")
            .HasColumnName("STATE");

        builder.HasOne(d => d.Category).WithMany(p => p.Productcategoryrelations)
            .HasForeignKey(d => d.Categoryid)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PRODUCTCATEGORYRELATION_CATEGORY");

        builder.HasOne(d => d.Product).WithMany(p => p.Productcategoryrelations)
            .HasForeignKey(d => d.Productid)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PRODUCTCATEGORYRELATION_PRODUCT");
    }
}