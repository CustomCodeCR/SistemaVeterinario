// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.HasKey(e => e.Id).HasName("SYS_C008044");

        builder.ToTable("PET");

        builder.HasIndex(e => e.Clientid, "IDX_PET_CLIENTID");

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("PETID");
        builder.Property(e => e.Age)
            .HasColumnType("NUMBER")
            .HasColumnName("AGE");
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
        builder.Property(e => e.Breed)
            .HasMaxLength(50)
            .HasColumnName("BREED");
        builder.Property(e => e.Clientid)
            .HasColumnType("NUMBER")
            .HasColumnName("CLIENTID");
        builder.Property(e => e.Name)
            .HasMaxLength(50)
            .HasColumnName("NAME");
        builder.Property(e => e.State)
            .HasColumnType("NUMBER")
            .HasColumnName("STATE");
        builder.Property(e => e.Type)
            .HasMaxLength(50)
            .HasColumnName("TYPE");

        builder.HasOne(d => d.Client).WithMany(p => p.Pets)
            .HasForeignKey(d => d.Clientid)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PET_CLIENT");
    }
}