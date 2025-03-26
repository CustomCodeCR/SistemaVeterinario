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
        builder.HasKey(e => e.Id).HasName("SYS_C008152");

        builder.ToTable("PURCHASEORDER");

        builder.HasIndex(e => e.Supplierid, "IDX_PURCHASEORDER_SUPPLIERID");

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("PURCHASEORDERID");
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