// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class ViewVaccineInfoConfiguration : IEntityTypeConfiguration<Viewvaccineinfo>
{
    public void Configure(EntityTypeBuilder<Viewvaccineinfo> builder)
    {
        builder
                .HasNoKey()
                .ToView("VIEWVACCINEINFO");

        builder.Property(e => e.Applicationdate)
            .HasColumnType("DATE")
            .HasColumnName("APPLICATIONDATE");
        builder.Property(e => e.Appliedvaccineid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("APPLIEDVACCINEID");
        builder.Property(e => e.Description)
            .HasMaxLength(100)
            .HasColumnName("DESCRIPTION");
        builder.Property(e => e.Petname)
            .HasMaxLength(50)
            .HasColumnName("PETNAME");
        builder.Property(e => e.Vaccinename)
            .HasMaxLength(50)
            .HasColumnName("VACCINENAME");
    }
}