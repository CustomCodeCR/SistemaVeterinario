// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class ViewClientInfo : IEntityTypeConfiguration<Viewclientinfo>
{
    public void Configure(EntityTypeBuilder<Viewclientinfo> builder)
    {
        builder
                .HasNoKey()
                .ToView("VIEWCLIENTINFO");

        builder.Property(e => e.Address)
            .HasMaxLength(100)
            .HasColumnName("ADDRESS");
        builder.Property(e => e.Auditcreatedate)
            .HasPrecision(7)
            .HasColumnName("AUDITCREATEDATE");
        builder.Property(e => e.Clientid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("CLIENTID");
        builder.Property(e => e.Email)
            .HasMaxLength(50)
            .HasColumnName("EMAIL");
        builder.Property(e => e.Firstname)
            .HasMaxLength(50)
            .HasColumnName("FIRSTNAME");
        builder.Property(e => e.Lastname)
            .HasMaxLength(50)
            .HasColumnName("LASTNAME");
        builder.Property(e => e.Phone)
            .HasMaxLength(20)
            .HasColumnName("PHONE");
        builder.Property(e => e.Userid)
            .HasColumnType("NUMBER")
            .HasColumnName("USERID");
    }
}