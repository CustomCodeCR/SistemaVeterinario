// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class PurchaseOrderConfiguration : IEntityTypeConfiguration<Purchaseorder>
{
    public void Configure(EntityTypeBuilder<Purchaseorder> builder)
    {
        builder.HasKey(e => e.Purchaseorderid).HasName("SYS_C008152");

        builder.ToTable("PURCHASEORDER");

        builder.HasIndex(e => e.Supplierid, "IDX_PURCHASEORDER_SUPPLIERID");

        builder.Property(e => e.Purchaseorderid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("PURCHASEORDERID");
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
        builder.Property(e => e.Orderdate)
            .HasColumnType("DATE")
            .HasColumnName("ORDERDATE");
        builder.Property(e => e.State)
            .HasColumnType("NUMBER")
            .HasColumnName("STATE");
        builder.Property(e => e.Status)
            .HasMaxLength(50)
            .ValueGeneratedOnAdd()
            .HasColumnName("STATUS");
        builder.Property(e => e.Supplierid)
            .HasColumnType("NUMBER")
            .HasColumnName("SUPPLIERID");

        builder.HasOne(d => d.Supplier).WithMany(p => p.Purchaseorders)
            .HasForeignKey(d => d.Supplierid)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PURCHASEORDER_SUPPLIER");
    }
}