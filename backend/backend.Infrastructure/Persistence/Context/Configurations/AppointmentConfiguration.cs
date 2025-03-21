// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(e => e.Appointmentid).HasName("SYS_C008061");

        builder.ToTable("APPOINTMENT");

        builder.HasIndex(e => e.Medicid, "IDX_APPOINTMENT_MEDICID");

        builder.HasIndex(e => e.Petid, "IDX_APPOINTMENT_PETID");

        builder.Property(e => e.Appointmentid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("APPOINTMENTID");
        builder.Property(e => e.Appointmentdate)
            .ValueGeneratedOnAdd()
            .HasColumnType("DATE")
            .HasColumnName("APPOINTMENTDATE");
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
        builder.Property(e => e.Medicid)
            .HasColumnType("NUMBER")
            .HasColumnName("MEDICID");
        builder.Property(e => e.Petid)
            .HasColumnType("NUMBER")
            .HasColumnName("PETID");
        builder.Property(e => e.Reason)
            .HasMaxLength(100)
            .HasColumnName("REASON");
        builder.Property(e => e.State)
            .HasColumnType("NUMBER")
            .HasColumnName("STATE");

        builder.HasOne(d => d.Medic).WithMany(p => p.Appointments)
            .HasForeignKey(d => d.Medicid)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_APPOINTMENT_MEDIC");

        builder.HasOne(d => d.Pet).WithMany(p => p.Appointments)
            .HasForeignKey(d => d.Petid)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_APPOINTMENT_PET");
    }
}