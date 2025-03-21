// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class AppointmentDetailConfiguration : IEntityTypeConfiguration<Appointmentdetail>
{
    public void Configure(EntityTypeBuilder<Appointmentdetail> builder)
    {
        builder.HasKey(e => e.Appointmentdetailid).HasName("SYS_C008071");

        builder.ToTable("APPOINTMENTDETAIL");

        builder.HasIndex(e => e.Appointmentid, "IDX_APPOINTMENTDETAIL_APPOINTMENTID");

        builder.Property(e => e.Appointmentdetailid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("APPOINTMENTDETAILID");
        builder.Property(e => e.Appointmentid)
            .HasColumnType("NUMBER")
            .HasColumnName("APPOINTMENTID");
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
        builder.Property(e => e.Diagnosis)
            .HasMaxLength(200)
            .HasColumnName("DIAGNOSIS");
        builder.Property(e => e.Observations)
            .HasMaxLength(200)
            .HasColumnName("OBSERVATIONS");
        builder.Property(e => e.State)
            .HasColumnType("NUMBER")
            .HasColumnName("STATE");
        builder.Property(e => e.Treatment)
            .HasMaxLength(200)
            .HasColumnName("TREATMENT");

        builder.HasOne(d => d.Appointment).WithMany(p => p.Appointmentdetails)
            .HasForeignKey(d => d.Appointmentid)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_APPOINTMENTDETAIL_APPOINTMENT");
    }
}