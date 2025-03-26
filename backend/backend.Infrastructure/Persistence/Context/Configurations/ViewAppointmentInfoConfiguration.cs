// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class ViewAppointmentInfoConfiguration : IEntityTypeConfiguration<Viewappointmentinfo>
{
    public void Configure(EntityTypeBuilder<Viewappointmentinfo> builder)
    {
        builder
                .HasNoKey()
                .ToView("VIEWAPPOINTMENTINFO");

        builder.Property(e => e.Appointmentdate)
            .HasColumnType("DATE")
            .HasColumnName("APPOINTMENTDATE");
        builder.Property(e => e.Appointmentid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("APPOINTMENTID");
        builder.Property(e => e.Medicphone)
            .HasMaxLength(20)
            .HasColumnName("MEDICPHONE");
        builder.Property(e => e.Medicspecialty)
            .HasMaxLength(50)
            .HasColumnName("MEDICSPECIALTY");
        builder.Property(e => e.Petname)
            .HasMaxLength(50)
            .HasColumnName("PETNAME");
        builder.Property(e => e.Reason)
            .HasMaxLength(100)
            .HasColumnName("REASON");
    }
}