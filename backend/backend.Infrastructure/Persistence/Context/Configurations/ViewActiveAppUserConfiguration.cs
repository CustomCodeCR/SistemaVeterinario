// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class ViewActiveAppUserConfiguration : IEntityTypeConfiguration<Viewactiveappuser>
{
    public void Configure(EntityTypeBuilder<Viewactiveappuser> builder)
    {
        builder
                .HasNoKey()
                .ToView("VIEWACTIVEAPPUSERS");

        builder.Property(e => e.Auditcreatedate)
            .HasPrecision(7)
            .HasColumnName("AUDITCREATEDATE");
        builder.Property(e => e.Auditcreateuser)
            .HasColumnType("NUMBER")
            .HasColumnName("AUDITCREATEUSER");
        builder.Property(e => e.Auditupdatedate)
            .HasPrecision(7)
            .HasColumnName("AUDITUPDATEDATE");
        builder.Property(e => e.Auditupdateuser)
            .HasColumnType("NUMBER")
            .HasColumnName("AUDITUPDATEUSER");
        builder.Property(e => e.Email)
            .HasMaxLength(50)
            .HasColumnName("EMAIL");
        builder.Property(e => e.Firstname)
            .HasMaxLength(50)
            .HasColumnName("FIRSTNAME");
        builder.Property(e => e.Lastname)
            .HasMaxLength(50)
            .HasColumnName("LASTNAME");
        builder.Property(e => e.Userid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("USERID");
        builder.Property(e => e.Username)
            .HasMaxLength(50)
            .HasColumnName("USERNAME");
        builder.Property(e => e.Usertype)
            .HasMaxLength(20)
            .HasColumnName("USERTYPE");
    }
}