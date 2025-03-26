// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class ViewPetDetailConfiguration : IEntityTypeConfiguration<Viewpetdetail>
{
    public void Configure(EntityTypeBuilder<Viewpetdetail> builder)
    {
        builder
                .HasNoKey()
                .ToView("VIEWPETDETAILS");

        builder.Property(e => e.Address)
            .HasMaxLength(100)
            .HasColumnName("ADDRESS");
        builder.Property(e => e.Age)
            .HasColumnType("NUMBER")
            .HasColumnName("AGE");
        builder.Property(e => e.Breed)
            .HasMaxLength(50)
            .HasColumnName("BREED");
        builder.Property(e => e.Clientid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("CLIENTID");
        builder.Property(e => e.Petid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("PETID");
        builder.Property(e => e.Petname)
            .HasMaxLength(50)
            .HasColumnName("PETNAME");
        builder.Property(e => e.Phone)
            .HasMaxLength(20)
            .HasColumnName("PHONE");
        builder.Property(e => e.Type)
            .HasMaxLength(50)
            .HasColumnName("TYPE");
    }
}