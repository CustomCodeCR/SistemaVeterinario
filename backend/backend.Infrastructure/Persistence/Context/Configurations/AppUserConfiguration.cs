// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<Appuser>
{
    public void Configure(EntityTypeBuilder<Appuser> builder)
    {
        builder.HasKey(e => e.Id).HasName("SYS_C008026");

        builder.ToTable("APPUSER");

        builder.HasIndex(e => e.Username, "SYS_C008027").IsUnique();

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("USERID");
        builder.Property(e => e.AuditCreateDate)
            .HasPrecision(7)
            .ValueGeneratedOnAdd()
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
        builder.Property(e => e.Email)
            .HasMaxLength(50)
            .ValueGeneratedOnAdd()
            .HasColumnName("EMAIL");
        builder.Property(e => e.Firstname)
            .HasMaxLength(50)
            .HasColumnName("FIRSTNAME");
        builder.Property(e => e.Lastname)
            .HasMaxLength(50)
            .HasColumnName("LASTNAME");
        builder.Property(e => e.Password)
            .HasMaxLength(256)
            .ValueGeneratedOnAdd()
            .HasColumnName("PASSWORD");
        builder.Property(e => e.State)
            .HasColumnType("NUMBER")
            .HasColumnName("STATE");
        builder.Property(e => e.Username)
            .HasMaxLength(50)
            .ValueGeneratedOnAdd()
            .HasColumnName("USERNAME");
        builder.Property(e => e.Usertype)
            .HasMaxLength(20)
            .HasColumnName("USERTYPE");
    }
}