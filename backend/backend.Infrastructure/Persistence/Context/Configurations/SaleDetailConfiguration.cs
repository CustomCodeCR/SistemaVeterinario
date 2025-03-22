// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class SaleDetailConfiguration : IEntityTypeConfiguration<Saledetail>
{
    public void Configure(EntityTypeBuilder<Saledetail> builder)
    {
        builder.HasKey(e => new { e.Saleid, e.Productid });

        builder.ToTable("SALEDETAIL");

        builder.HasIndex(e => e.Productid, "IDX_SALEDETAIL_PRODUCTID");

        builder.HasIndex(e => e.Saleid, "IDX_SALEDETAIL_SALEID");

        builder.Property(e => e.Saleid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("SALEID");
        builder.Property(e => e.Productid)
            .HasColumnType("NUMBER")
            .HasColumnName("PRODUCTID");
        builder.Property(e => e.AuditCreateDate)
            .HasPrecision(7)
            .HasColumnName("AUDITCREATEDATE");
        builder.Property(e => e.AuditCreateUser)
            .HasColumnType("NUMBER")
            .HasColumnName("AUDITCREATEUSER");
        builder.Property(e => e.AuditDeleteDate)
            .HasPrecision(7)
            .HasColumnName("AUDITDELETEDATE");
        builder.Property(e => e.AuditDeleteUser)
            .HasColumnType("NUMBER")
            .HasColumnName("AUDITDELETEUSER");
        builder.Property(e => e.AuditUpdateDate)
            .HasPrecision(7)
            .HasColumnName("AUDITUPDATEDATE");
        builder.Property(e => e.AuditUpdateUser)
            .HasColumnType("NUMBER")
            .HasColumnName("AUDITUPDATEUSER");
        builder.Property(e => e.Price)
            .HasColumnType("NUMBER(10,2)")
            .HasColumnName("PRICE");
        builder.Property(e => e.Quantity)
            .HasColumnType("NUMBER")
            .HasColumnName("QUANTITY");
        builder.Property(e => e.State)
            .HasColumnType("NUMBER")
            .HasColumnName("STATE");

        builder.HasOne(d => d.Product).WithMany(p => p.Saledetails)
            .HasForeignKey(d => d.Productid)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_SALEDETAIL_PRODUCT");

        builder.HasOne(d => d.Sale).WithMany(p => p.Saledetails)
            .HasForeignKey(d => d.Saleid)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_SALEDETAIL_SALE");
    }
}