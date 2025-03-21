// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(e => e.Id).HasName("SYS_C008034");

        builder.ToTable("CLIENT");

        builder.HasIndex(e => e.Userid, "IDX_CLIENT_USERID");

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("CLIENTID");
        builder.Property(e => e.Address)
            .HasMaxLength(100)
            .HasColumnName("ADDRESS");
        builder.Property(e => e.AuditCreateDate)
            .HasPrecision(7)
            .HasColumnName("AUDITCREATEDATE");
        builder.Property(e => e.AuditCreateUser)
            .HasColumnType("NUMBER")
            .HasColumnName("AUDITCREATEUSER");
        builder.Property(e => e.AuditDeleteDate)
            .HasPrecision(7)
            .HasColumnName("AUDITDELETEDATE");
        builder.Property(e => e.AuditCreateUser)
            .HasColumnType("NUMBER")
            .HasColumnName("AUDITDELETEUSER");
        builder.Property(e => e.AuditUpdateDate)
            .HasPrecision(7)
            .HasColumnName("AUDITUPDATEDATE");
        builder.Property(e => e.AuditUpdateUser)
            .HasColumnType("NUMBER")
            .HasColumnName("AUDITUPDATEUSER");
        builder.Property(e => e.Phone)
            .HasMaxLength(20)
            .HasColumnName("PHONE");
        builder.Property(e => e.State)
            .HasColumnType("NUMBER")
            .HasColumnName("STATE");
        builder.Property(e => e.Userid)
            .HasColumnType("NUMBER")
            .HasColumnName("USERID");

        builder.HasOne(d => d.User).WithMany(p => p.Clients)
            .HasForeignKey(d => d.Userid)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_CLIENT_USER");
    }
}