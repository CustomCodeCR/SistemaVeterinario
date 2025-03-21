// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class AppliedVaccinesConfiguration : IEntityTypeConfiguration<Appliedvaccine>
{
    public void Configure(EntityTypeBuilder<Appliedvaccine> builder)
    {
        builder.HasKey(e => e.Id).HasName("SYS_C008086");

        builder.ToTable("APPLIEDVACCINE");

        builder.HasIndex(e => e.Petid, "IDX_APPLIEDVACCINE_PETID");

        builder.HasIndex(e => e.Vaccineid, "IDX_APPLIEDVACCINE_VACCINEID");

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("APPLIEDVACCINEID");
        builder.Property(e => e.Applicationdate)
            .HasColumnType("DATE")
            .HasColumnName("APPLICATIONDATE");
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
        builder.Property(e => e.Petid)
            .HasColumnType("NUMBER")
            .HasColumnName("PETID");
        builder.Property(e => e.State)
            .HasColumnType("NUMBER")
            .HasColumnName("STATE");
        builder.Property(e => e.Vaccineid)
            .HasColumnType("NUMBER")
            .HasColumnName("VACCINEID");

        builder.HasOne(d => d.Pet).WithMany(p => p.Appliedvaccines)
            .HasForeignKey(d => d.Petid)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_APPLIEDVACCINE_PET");

        builder.HasOne(d => d.Vaccine).WithMany(p => p.Appliedvaccines)
            .HasForeignKey(d => d.Vaccineid)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_APPLIEDVACCINE_VACCINE");
    }
}