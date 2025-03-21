// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.HasKey(e => e.Inventoryid).HasName("SYS_C008102");

        builder.ToTable("INVENTORY");

        builder.HasIndex(e => e.Productid, "IDX_INVENTORY_PRODUCTID");

        builder.Property(e => e.Inventoryid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("INVENTORYID");
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
        builder.Property(e => e.Productid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("PRODUCTID");
        builder.Property(e => e.Quantity)
            .HasColumnType("NUMBER")
            .HasColumnName("QUANTITY");
        builder.Property(e => e.State)
            .HasColumnType("NUMBER")
            .HasColumnName("STATE");
        builder.Property(e => e.Updatedate)
            .HasColumnType("DATE")
            .HasColumnName("UPDATEDATE");

        builder.HasOne(d => d.Product).WithMany(p => p.Inventories)
            .HasForeignKey(d => d.Productid)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_INVENTORY_PRODUCT");
    }
}