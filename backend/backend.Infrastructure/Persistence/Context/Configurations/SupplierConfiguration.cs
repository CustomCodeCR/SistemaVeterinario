// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasKey(e => e.Id).HasName("SYS_C008145");

        builder.ToTable("SUPPLIER");

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("SUPPLIERID");
        builder.Property(e => e.Address)
            .HasMaxLength(100)
            .HasColumnName("ADDRESS");
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
        builder.Property(e => e.Contact)
            .HasMaxLength(50)
            .ValueGeneratedOnAdd()
            .HasColumnName("CONTACT");
        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .HasColumnName("NAME");
        builder.Property(e => e.Phone)
            .HasMaxLength(20)
            .ValueGeneratedOnAdd()
            .HasColumnName("PHONE");
        builder.Property(e => e.State)
            .HasColumnType("NUMBER")
            .HasColumnName("STATE");
    }
}