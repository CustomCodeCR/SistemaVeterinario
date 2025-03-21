// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class PurchaseOrderDetailConfiguration : IEntityTypeConfiguration<Purchaseorderdetail>
{
    public void Configure(EntityTypeBuilder<Purchaseorderdetail> builder)
    {
        builder.HasKey(e => new { e.Purchaseorderid, e.Productid });

        builder.ToTable("PURCHASEORDERDETAIL");

        builder.HasIndex(e => e.Productid, "IDX_PURCHASEORDERDETAIL_PRODUCTID");

        builder.HasIndex(e => e.Purchaseorderid, "IDX_PURCHASEORDERDETAIL_PURCHASEORDERID");

        builder.Property(e => e.Purchaseorderid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("PURCHASEORDERID");
        builder.Property(e => e.Productid)
            .HasColumnType("NUMBER")
            .HasColumnName("PRODUCTID");
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
        builder.Property(e => e.Quantity)
            .HasColumnType("NUMBER")
            .HasColumnName("QUANTITY");
        builder.Property(e => e.State)
            .HasColumnType("NUMBER")
            .HasColumnName("STATE");
        builder.Property(e => e.Unitprice)
            .HasColumnType("NUMBER(10,2)")
            .HasColumnName("UNITPRICE");

        builder.HasOne(d => d.Product).WithMany(p => p.Purchaseorderdetails)
            .HasForeignKey(d => d.Productid)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PURCHASEORDERDETAIL_PRODUCT");

        builder.HasOne(d => d.Purchaseorder).WithMany(p => p.Purchaseorderdetails)
            .HasForeignKey(d => d.Purchaseorderid)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PURCHASEORDERDETAIL_ORDER");
    }
}