using System;
using System.Collections.Generic;
using System.Data;
using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace backend.Infrastructure.Persistence.Context;

public partial class ApplicationDbContext : DbContext
{
    private readonly string _connectionString;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, string connectionString)
        : base(options)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection => new OracleConnection(_connectionString);

    public virtual DbSet<Appliedvaccine> Appliedvaccines { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Appointmentdetail> Appointmentdetails { get; set; }

    public virtual DbSet<Appuser> Appusers { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Medic> Medics { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Pet> Pets { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Productcategory> Productcategories { get; set; }

    public virtual DbSet<Productcategoryrelation> Productcategoryrelations { get; set; }

    public virtual DbSet<Purchaseorder> Purchaseorders { get; set; }

    public virtual DbSet<Purchaseorderdetail> Purchaseorderdetails { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Saledetail> Saledetails { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Vaccine> Vaccines { get; set; }

    public virtual DbSet<Viewactiveappuser> Viewactiveappusers { get; set; }

    public virtual DbSet<Viewappointmentinfo> Viewappointmentinfos { get; set; }

    public virtual DbSet<Viewclientinfo> Viewclientinfos { get; set; }

    public virtual DbSet<Viewpaymentsummary> Viewpaymentsummaries { get; set; }

    public virtual DbSet<Viewpetdetail> Viewpetdetails { get; set; }

    public virtual DbSet<Viewproductcategorymapping> Viewproductcategorymappings { get; set; }

    public virtual DbSet<Viewproductinventory> Viewproductinventories { get; set; }

    public virtual DbSet<Viewpurchaseordersummary> Viewpurchaseordersummaries { get; set; }

    public virtual DbSet<Viewsalesummary> Viewsalesummaries { get; set; }

    public virtual DbSet<Viewvaccineinfo> Viewvaccineinfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("User Id=VETFRIENDS;Password=S0port32024;Data Source=oracle.customcodecr.com:1521/ORCLDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("VETFRIENDS")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Appliedvaccine>(entity =>
        {
            entity.HasKey(e => e.Appliedvaccineid).HasName("SYS_C008086");

            entity.ToTable("APPLIEDVACCINE");

            entity.HasIndex(e => e.Petid, "IDX_APPLIEDVACCINE_PETID");

            entity.HasIndex(e => e.Vaccineid, "IDX_APPLIEDVACCINE_VACCINEID");

            entity.Property(e => e.Appliedvaccineid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("APPLIEDVACCINEID");
            entity.Property(e => e.Applicationdate)
                .HasColumnType("DATE")
                .HasColumnName("APPLICATIONDATE");
            entity.Property(e => e.Auditcreatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.Auditcreateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITCREATEUSER");
            entity.Property(e => e.Auditdeletedate)
                .HasPrecision(7)
                .HasColumnName("AUDITDELETEDATE");
            entity.Property(e => e.Auditdeleteuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITDELETEUSER");
            entity.Property(e => e.Auditupdatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITUPDATEDATE");
            entity.Property(e => e.Auditupdateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITUPDATEUSER");
            entity.Property(e => e.Petid)
                .HasColumnType("NUMBER")
                .HasColumnName("PETID");
            entity.Property(e => e.State)
                .HasColumnType("NUMBER")
                .HasColumnName("STATE");
            entity.Property(e => e.Vaccineid)
                .HasColumnType("NUMBER")
                .HasColumnName("VACCINEID");

            entity.HasOne(d => d.Pet).WithMany(p => p.Appliedvaccines)
                .HasForeignKey(d => d.Petid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_APPLIEDVACCINE_PET");

            entity.HasOne(d => d.Vaccine).WithMany(p => p.Appliedvaccines)
                .HasForeignKey(d => d.Vaccineid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_APPLIEDVACCINE_VACCINE");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Appointmentid).HasName("SYS_C008061");

            entity.ToTable("APPOINTMENT");

            entity.HasIndex(e => e.Medicid, "IDX_APPOINTMENT_MEDICID");

            entity.HasIndex(e => e.Petid, "IDX_APPOINTMENT_PETID");

            entity.Property(e => e.Appointmentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("APPOINTMENTID");
            entity.Property(e => e.Appointmentdate)
                .ValueGeneratedOnAdd()
                .HasColumnType("DATE")
                .HasColumnName("APPOINTMENTDATE");
            entity.Property(e => e.Auditcreatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.Auditcreateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITCREATEUSER");
            entity.Property(e => e.Auditdeletedate)
                .HasPrecision(7)
                .HasColumnName("AUDITDELETEDATE");
            entity.Property(e => e.Auditdeleteuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITDELETEUSER");
            entity.Property(e => e.Auditupdatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITUPDATEDATE");
            entity.Property(e => e.Auditupdateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITUPDATEUSER");
            entity.Property(e => e.Medicid)
                .HasColumnType("NUMBER")
                .HasColumnName("MEDICID");
            entity.Property(e => e.Petid)
                .HasColumnType("NUMBER")
                .HasColumnName("PETID");
            entity.Property(e => e.Reason)
                .HasMaxLength(100)
                .HasColumnName("REASON");
            entity.Property(e => e.State)
                .HasColumnType("NUMBER")
                .HasColumnName("STATE");

            entity.HasOne(d => d.Medic).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.Medicid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_APPOINTMENT_MEDIC");

            entity.HasOne(d => d.Pet).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.Petid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_APPOINTMENT_PET");
        });

        modelBuilder.Entity<Appointmentdetail>(entity =>
        {
            entity.HasKey(e => e.Appointmentdetailid).HasName("SYS_C008071");

            entity.ToTable("APPOINTMENTDETAIL");

            entity.HasIndex(e => e.Appointmentid, "IDX_APPOINTMENTDETAIL_APPOINTMENTID");

            entity.Property(e => e.Appointmentdetailid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("APPOINTMENTDETAILID");
            entity.Property(e => e.Appointmentid)
                .HasColumnType("NUMBER")
                .HasColumnName("APPOINTMENTID");
            entity.Property(e => e.Auditcreatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.Auditcreateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITCREATEUSER");
            entity.Property(e => e.Auditdeletedate)
                .HasPrecision(7)
                .HasColumnName("AUDITDELETEDATE");
            entity.Property(e => e.Auditdeleteuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITDELETEUSER");
            entity.Property(e => e.Auditupdatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITUPDATEDATE");
            entity.Property(e => e.Auditupdateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITUPDATEUSER");
            entity.Property(e => e.Diagnosis)
                .HasMaxLength(200)
                .HasColumnName("DIAGNOSIS");
            entity.Property(e => e.Observations)
                .HasMaxLength(200)
                .HasColumnName("OBSERVATIONS");
            entity.Property(e => e.State)
                .HasColumnType("NUMBER")
                .HasColumnName("STATE");
            entity.Property(e => e.Treatment)
                .HasMaxLength(200)
                .HasColumnName("TREATMENT");

            entity.HasOne(d => d.Appointment).WithMany(p => p.Appointmentdetails)
                .HasForeignKey(d => d.Appointmentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_APPOINTMENTDETAIL_APPOINTMENT");
        });

        modelBuilder.Entity<Appuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008026");

            entity.ToTable("APPUSER");

            entity.HasIndex(e => e.Username, "SYS_C008027").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");
            entity.Property(e => e.AuditCreateDate)
                .HasPrecision(7)
                .ValueGeneratedOnAdd()
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.AuditCreateUser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITCREATEUSER");
            entity.Property(e => e.AuditDeleteDate)
                .HasPrecision(7)
                .HasColumnName("AUDITDELETEDATE");
            entity.Property(e => e.AuditDeleteUser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITDELETEUSER");
            entity.Property(e => e.AuditUpdateDate)
                .HasPrecision(7)
                .HasColumnName("AUDITUPDATEDATE");
            entity.Property(e => e.AuditUpdateUser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITUPDATEUSER");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .ValueGeneratedOnAdd()
                .HasColumnName("EMAIL");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("LASTNAME");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .ValueGeneratedOnAdd()
                .HasColumnName("PASSWORD");
            entity.Property(e => e.State)
                .HasColumnType("NUMBER")
                .HasColumnName("STATE");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .ValueGeneratedOnAdd()
                .HasColumnName("USERNAME");
            entity.Property(e => e.Usertype)
                .HasMaxLength(20)
                .HasColumnName("USERTYPE");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008034");

            entity.ToTable("CLIENT");

            entity.HasIndex(e => e.Userid, "IDX_CLIENT_USERID");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CLIENTID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.AuditCreateDate)
                .HasPrecision(7)
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.AuditCreateUser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITCREATEUSER");
            entity.Property(e => e.AuditDeleteDate)
                .HasPrecision(7)
                .HasColumnName("AUDITDELETEDATE");
            entity.Property(e => e.AuditCreateUser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITDELETEUSER");
            entity.Property(e => e.AuditUpdateDate)
                .HasPrecision(7)
                .HasColumnName("AUDITUPDATEDATE");
            entity.Property(e => e.AuditUpdateUser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITUPDATEUSER");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("PHONE");
            entity.Property(e => e.State)
                .HasColumnType("NUMBER")
                .HasColumnName("STATE");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");

            entity.HasOne(d => d.User).WithMany(p => p.Clients)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CLIENT_USER");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Inventoryid).HasName("SYS_C008102");

            entity.ToTable("INVENTORY");

            entity.HasIndex(e => e.Productid, "IDX_INVENTORY_PRODUCTID");

            entity.Property(e => e.Inventoryid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("INVENTORYID");
            entity.Property(e => e.Auditcreatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.Auditcreateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITCREATEUSER");
            entity.Property(e => e.Auditdeletedate)
                .HasPrecision(7)
                .HasColumnName("AUDITDELETEDATE");
            entity.Property(e => e.Auditdeleteuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITDELETEUSER");
            entity.Property(e => e.Auditupdatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITUPDATEDATE");
            entity.Property(e => e.Auditupdateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITUPDATEUSER");
            entity.Property(e => e.Productid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PRODUCTID");
            entity.Property(e => e.Quantity)
                .HasColumnType("NUMBER")
                .HasColumnName("QUANTITY");
            entity.Property(e => e.State)
                .HasColumnType("NUMBER")
                .HasColumnName("STATE");
            entity.Property(e => e.Updatedate)
                .HasColumnType("DATE")
                .HasColumnName("UPDATEDATE");

            entity.HasOne(d => d.Product).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_INVENTORY_PRODUCT");
        });

        modelBuilder.Entity<Medic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008052");

            entity.ToTable("MEDIC");

            entity.HasIndex(e => e.Userid, "IDX_MEDIC_USERID");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("MEDICID");
            entity.Property(e => e.AuditCreateDate)
                .HasPrecision(7)
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.AuditCreateUser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITCREATEUSER");
            entity.Property(e => e.AuditDeleteDate)
                .HasPrecision(7)
                .HasColumnName("AUDITDELETEDATE");
            entity.Property(e => e.AuditCreateUser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITDELETEUSER");
            entity.Property(e => e.AuditUpdateDate)
                .HasPrecision(7)
                .HasColumnName("AUDITUPDATEDATE");
            entity.Property(e => e.AuditUpdateUser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITUPDATEUSER");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("PHONE");
            entity.Property(e => e.Specialty)
                .HasMaxLength(50)
                .HasColumnName("SPECIALTY");
            entity.Property(e => e.State)
                .HasColumnType("NUMBER")
                .HasColumnName("STATE");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");

            entity.HasOne(d => d.User).WithMany(p => p.Medics)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MEDIC_USER");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Paymentid).HasName("SYS_C008125");

            entity.ToTable("PAYMENT");

            entity.HasIndex(e => e.Saleid, "IDX_PAYMENT_SALEID");

            entity.Property(e => e.Paymentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PAYMENTID");
            entity.Property(e => e.Amount)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("AMOUNT");
            entity.Property(e => e.Auditcreatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.Auditcreateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITCREATEUSER");
            entity.Property(e => e.Auditdeletedate)
                .HasPrecision(7)
                .HasColumnName("AUDITDELETEDATE");
            entity.Property(e => e.Auditdeleteuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITDELETEUSER");
            entity.Property(e => e.Auditupdatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITUPDATEDATE");
            entity.Property(e => e.Auditupdateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITUPDATEUSER");
            entity.Property(e => e.Paymentdate)
                .HasColumnType("DATE")
                .HasColumnName("PAYMENTDATE");
            entity.Property(e => e.Paymenttype)
                .HasMaxLength(50)
                .HasColumnName("PAYMENTTYPE");
            entity.Property(e => e.Saleid)
                .HasColumnType("NUMBER")
                .HasColumnName("SALEID");
            entity.Property(e => e.State)
                .HasColumnType("NUMBER")
                .HasColumnName("STATE");

            entity.HasOne(d => d.Sale).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Saleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PAYMENT_SALE");
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008044");

            entity.ToTable("PET");

            entity.HasIndex(e => e.Clientid, "IDX_PET_CLIENTID");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PETID");
            entity.Property(e => e.Age)
                .HasColumnType("NUMBER")
                .HasColumnName("AGE");
            entity.Property(e => e.AuditCreateDate)
                .HasPrecision(7)
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.AuditCreateUser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITCREATEUSER");
            entity.Property(e => e.AuditDeleteDate)
                .HasPrecision(7)
                .HasColumnName("AUDITDELETEDATE");
            entity.Property(e => e.AuditCreateUser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITDELETEUSER");
            entity.Property(e => e.AuditUpdateDate)
                .HasPrecision(7)
                .HasColumnName("AUDITUPDATEDATE");
            entity.Property(e => e.AuditUpdateUser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITUPDATEUSER");
            entity.Property(e => e.Breed)
                .HasMaxLength(50)
                .HasColumnName("BREED");
            entity.Property(e => e.Clientid)
                .HasColumnType("NUMBER")
                .HasColumnName("CLIENTID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.State)
                .HasColumnType("NUMBER")
                .HasColumnName("STATE");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("TYPE");

            entity.HasOne(d => d.Client).WithMany(p => p.Pets)
                .HasForeignKey(d => d.Clientid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PET_CLIENT");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Productid).HasName("SYS_C008095");

            entity.ToTable("PRODUCT");

            entity.Property(e => e.Productid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PRODUCTID");
            entity.Property(e => e.Auditcreatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.Auditcreateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITCREATEUSER");
            entity.Property(e => e.Auditdeletedate)
                .HasPrecision(7)
                .HasColumnName("AUDITDELETEDATE");
            entity.Property(e => e.Auditdeleteuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITDELETEUSER");
            entity.Property(e => e.Auditupdatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITUPDATEDATE");
            entity.Property(e => e.Auditupdateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITUPDATEUSER");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.Price)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("PRICE");
            entity.Property(e => e.State)
                .HasColumnType("NUMBER")
                .HasColumnName("STATE");
        });

        modelBuilder.Entity<Productcategory>(entity =>
        {
            entity.HasKey(e => e.Categoryid).HasName("SYS_C008132");

            entity.ToTable("PRODUCTCATEGORY");

            entity.Property(e => e.Categoryid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CATEGORYID");
            entity.Property(e => e.Auditcreatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.Auditcreateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITCREATEUSER");
            entity.Property(e => e.Auditdeletedate)
                .HasPrecision(7)
                .HasColumnName("AUDITDELETEDATE");
            entity.Property(e => e.Auditdeleteuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITDELETEUSER");
            entity.Property(e => e.Auditupdatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITUPDATEDATE");
            entity.Property(e => e.Auditupdateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITUPDATEUSER");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(50)
                .HasColumnName("CATEGORYNAME");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.State)
                .HasColumnType("NUMBER")
                .HasColumnName("STATE");
        });

        modelBuilder.Entity<Productcategoryrelation>(entity =>
        {
            entity.HasKey(e => new { e.Productid, e.Categoryid });

            entity.ToTable("PRODUCTCATEGORYRELATION");

            entity.HasIndex(e => e.Categoryid, "IDX_PRODUCTCATEGORYRELATION_CATEGORYID");

            entity.HasIndex(e => e.Productid, "IDX_PRODUCTCATEGORYRELATION_PRODUCTID");

            entity.Property(e => e.Productid)
                .HasColumnType("NUMBER")
                .HasColumnName("PRODUCTID");
            entity.Property(e => e.Categoryid)
                .HasColumnType("NUMBER")
                .HasColumnName("CATEGORYID");
            entity.Property(e => e.Auditcreatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.Auditcreateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITCREATEUSER");
            entity.Property(e => e.Auditdeletedate)
                .HasPrecision(7)
                .HasColumnName("AUDITDELETEDATE");
            entity.Property(e => e.Auditdeleteuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITDELETEUSER");
            entity.Property(e => e.Auditupdatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITUPDATEDATE");
            entity.Property(e => e.Auditupdateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITUPDATEUSER");
            entity.Property(e => e.State)
                .HasColumnType("NUMBER")
                .HasColumnName("STATE");

            entity.HasOne(d => d.Category).WithMany(p => p.Productcategoryrelations)
                .HasForeignKey(d => d.Categoryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCTCATEGORYRELATION_CATEGORY");

            entity.HasOne(d => d.Product).WithMany(p => p.Productcategoryrelations)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCTCATEGORYRELATION_PRODUCT");
        });

        modelBuilder.Entity<Purchaseorder>(entity =>
        {
            entity.HasKey(e => e.Purchaseorderid).HasName("SYS_C008152");

            entity.ToTable("PURCHASEORDER");

            entity.HasIndex(e => e.Supplierid, "IDX_PURCHASEORDER_SUPPLIERID");

            entity.Property(e => e.Purchaseorderid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PURCHASEORDERID");
            entity.Property(e => e.Auditcreatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.Auditcreateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITCREATEUSER");
            entity.Property(e => e.Auditdeletedate)
                .HasPrecision(7)
                .HasColumnName("AUDITDELETEDATE");
            entity.Property(e => e.Auditdeleteuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITDELETEUSER");
            entity.Property(e => e.Auditupdatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITUPDATEDATE");
            entity.Property(e => e.Auditupdateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITUPDATEUSER");
            entity.Property(e => e.Orderdate)
                .HasColumnType("DATE")
                .HasColumnName("ORDERDATE");
            entity.Property(e => e.State)
                .HasColumnType("NUMBER")
                .HasColumnName("STATE");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .ValueGeneratedOnAdd()
                .HasColumnName("STATUS");
            entity.Property(e => e.Supplierid)
                .HasColumnType("NUMBER")
                .HasColumnName("SUPPLIERID");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Purchaseorders)
                .HasForeignKey(d => d.Supplierid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PURCHASEORDER_SUPPLIER");
        });

        modelBuilder.Entity<Purchaseorderdetail>(entity =>
        {
            entity.HasKey(e => new { e.Purchaseorderid, e.Productid });

            entity.ToTable("PURCHASEORDERDETAIL");

            entity.HasIndex(e => e.Productid, "IDX_PURCHASEORDERDETAIL_PRODUCTID");

            entity.HasIndex(e => e.Purchaseorderid, "IDX_PURCHASEORDERDETAIL_PURCHASEORDERID");

            entity.Property(e => e.Purchaseorderid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PURCHASEORDERID");
            entity.Property(e => e.Productid)
                .HasColumnType("NUMBER")
                .HasColumnName("PRODUCTID");
            entity.Property(e => e.Auditcreatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.Auditcreateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITCREATEUSER");
            entity.Property(e => e.Auditdeletedate)
                .HasPrecision(7)
                .HasColumnName("AUDITDELETEDATE");
            entity.Property(e => e.Auditdeleteuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITDELETEUSER");
            entity.Property(e => e.Auditupdatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITUPDATEDATE");
            entity.Property(e => e.Auditupdateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITUPDATEUSER");
            entity.Property(e => e.Quantity)
                .HasColumnType("NUMBER")
                .HasColumnName("QUANTITY");
            entity.Property(e => e.State)
                .HasColumnType("NUMBER")
                .HasColumnName("STATE");
            entity.Property(e => e.Unitprice)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("UNITPRICE");

            entity.HasOne(d => d.Product).WithMany(p => p.Purchaseorderdetails)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PURCHASEORDERDETAIL_PRODUCT");

            entity.HasOne(d => d.Purchaseorder).WithMany(p => p.Purchaseorderdetails)
                .HasForeignKey(d => d.Purchaseorderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PURCHASEORDERDETAIL_ORDER");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Saleid).HasName("SYS_C008109");

            entity.ToTable("SALE");

            entity.HasIndex(e => e.Clientid, "IDX_SALE_CLIENTID");

            entity.Property(e => e.Saleid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("SALEID");
            entity.Property(e => e.Auditcreatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.Auditcreateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITCREATEUSER");
            entity.Property(e => e.Auditdeletedate)
                .HasPrecision(7)
                .HasColumnName("AUDITDELETEDATE");
            entity.Property(e => e.Auditdeleteuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITDELETEUSER");
            entity.Property(e => e.Auditupdatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITUPDATEDATE");
            entity.Property(e => e.Auditupdateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITUPDATEUSER");
            entity.Property(e => e.Clientid)
                .HasColumnType("NUMBER")
                .HasColumnName("CLIENTID");
            entity.Property(e => e.Saledate)
                .HasColumnType("DATE")
                .HasColumnName("SALEDATE");
            entity.Property(e => e.State)
                .HasColumnType("NUMBER")
                .HasColumnName("STATE");

            entity.HasOne(d => d.Client).WithMany(p => p.Sales)
                .HasForeignKey(d => d.Clientid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SALE_CLIENT");
        });

        modelBuilder.Entity<Saledetail>(entity =>
        {
            entity.HasKey(e => new { e.Saleid, e.Productid });

            entity.ToTable("SALEDETAIL");

            entity.HasIndex(e => e.Productid, "IDX_SALEDETAIL_PRODUCTID");

            entity.HasIndex(e => e.Saleid, "IDX_SALEDETAIL_SALEID");

            entity.Property(e => e.Saleid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("SALEID");
            entity.Property(e => e.Productid)
                .HasColumnType("NUMBER")
                .HasColumnName("PRODUCTID");
            entity.Property(e => e.Auditcreatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.Auditcreateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITCREATEUSER");
            entity.Property(e => e.Auditdeletedate)
                .HasPrecision(7)
                .HasColumnName("AUDITDELETEDATE");
            entity.Property(e => e.Auditdeleteuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITDELETEUSER");
            entity.Property(e => e.Auditupdatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITUPDATEDATE");
            entity.Property(e => e.Auditupdateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITUPDATEUSER");
            entity.Property(e => e.Price)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("PRICE");
            entity.Property(e => e.Quantity)
                .HasColumnType("NUMBER")
                .HasColumnName("QUANTITY");
            entity.Property(e => e.State)
                .HasColumnType("NUMBER")
                .HasColumnName("STATE");

            entity.HasOne(d => d.Product).WithMany(p => p.Saledetails)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SALEDETAIL_PRODUCT");

            entity.HasOne(d => d.Sale).WithMany(p => p.Saledetails)
                .HasForeignKey(d => d.Saleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SALEDETAIL_SALE");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Supplierid).HasName("SYS_C008145");

            entity.ToTable("SUPPLIER");

            entity.Property(e => e.Supplierid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("SUPPLIERID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Auditcreatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.Auditcreateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITCREATEUSER");
            entity.Property(e => e.Auditdeletedate)
                .HasPrecision(7)
                .HasColumnName("AUDITDELETEDATE");
            entity.Property(e => e.Auditdeleteuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITDELETEUSER");
            entity.Property(e => e.Auditupdatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITUPDATEDATE");
            entity.Property(e => e.Auditupdateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITUPDATEUSER");
            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .ValueGeneratedOnAdd()
                .HasColumnName("CONTACT");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .ValueGeneratedOnAdd()
                .HasColumnName("PHONE");
            entity.Property(e => e.State)
                .HasColumnType("NUMBER")
                .HasColumnName("STATE");
        });

        modelBuilder.Entity<Vaccine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008079");

            entity.ToTable("VACCINE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("VACCINEID");
            entity.Property(e => e.AuditCreateDate)
                .HasPrecision(7)
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.AuditCreateUser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITCREATEUSER");
            entity.Property(e => e.AuditDeleteDate)
                .HasPrecision(7)
                .HasColumnName("AUDITDELETEDATE");
            entity.Property(e => e.AuditCreateUser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITDELETEUSER");
            entity.Property(e => e.AuditUpdateDate)
                .HasPrecision(7)
                .HasColumnName("AUDITUPDATEDATE");
            entity.Property(e => e.AuditUpdateUser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITUPDATEUSER");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.State)
                .HasColumnType("NUMBER")
                .HasColumnName("STATE");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("TYPE");
            entity.Property(e => e.Vaccinename)
                .HasMaxLength(50)
                .HasColumnName("VACCINENAME");
        });

        modelBuilder.Entity<Viewactiveappuser>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEWACTIVEAPPUSERS");

            entity.Property(e => e.Auditcreatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.Auditcreateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITCREATEUSER");
            entity.Property(e => e.Auditupdatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITUPDATEDATE");
            entity.Property(e => e.Auditupdateuser)
                .HasColumnType("NUMBER")
                .HasColumnName("AUDITUPDATEUSER");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("LASTNAME");
            entity.Property(e => e.Userid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("USERNAME");
            entity.Property(e => e.Usertype)
                .HasMaxLength(20)
                .HasColumnName("USERTYPE");
        });

        modelBuilder.Entity<Viewappointmentinfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEWAPPOINTMENTINFO");

            entity.Property(e => e.Appointmentdate)
                .HasColumnType("DATE")
                .HasColumnName("APPOINTMENTDATE");
            entity.Property(e => e.Appointmentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("APPOINTMENTID");
            entity.Property(e => e.Medicphone)
                .HasMaxLength(20)
                .HasColumnName("MEDICPHONE");
            entity.Property(e => e.Medicspecialty)
                .HasMaxLength(50)
                .HasColumnName("MEDICSPECIALTY");
            entity.Property(e => e.Petname)
                .HasMaxLength(50)
                .HasColumnName("PETNAME");
            entity.Property(e => e.Reason)
                .HasMaxLength(100)
                .HasColumnName("REASON");
        });

        modelBuilder.Entity<Viewclientinfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEWCLIENTINFO");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Auditcreatedate)
                .HasPrecision(7)
                .HasColumnName("AUDITCREATEDATE");
            entity.Property(e => e.Clientid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CLIENTID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("LASTNAME");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("PHONE");
            entity.Property(e => e.Userid)
                .HasColumnType("NUMBER")
                .HasColumnName("USERID");
        });

        modelBuilder.Entity<Viewpaymentsummary>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEWPAYMENTSUMMARY");

            entity.Property(e => e.Amount)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("AMOUNT");
            entity.Property(e => e.Paymentdate)
                .HasColumnType("DATE")
                .HasColumnName("PAYMENTDATE");
            entity.Property(e => e.Paymentid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PAYMENTID");
            entity.Property(e => e.Paymenttype)
                .HasMaxLength(50)
                .HasColumnName("PAYMENTTYPE");
            entity.Property(e => e.Saleid)
                .HasColumnType("NUMBER")
                .HasColumnName("SALEID");
        });

        modelBuilder.Entity<Viewpetdetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEWPETDETAILS");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Age)
                .HasColumnType("NUMBER")
                .HasColumnName("AGE");
            entity.Property(e => e.Breed)
                .HasMaxLength(50)
                .HasColumnName("BREED");
            entity.Property(e => e.Clientid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CLIENTID");
            entity.Property(e => e.Petid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PETID");
            entity.Property(e => e.Petname)
                .HasMaxLength(50)
                .HasColumnName("PETNAME");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("PHONE");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("TYPE");
        });

        modelBuilder.Entity<Viewproductcategorymapping>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEWPRODUCTCATEGORYMAPPING");

            entity.Property(e => e.Categoryid)
                .HasColumnType("NUMBER")
                .HasColumnName("CATEGORYID");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(50)
                .HasColumnName("CATEGORYNAME");
            entity.Property(e => e.Productid)
                .HasColumnType("NUMBER")
                .HasColumnName("PRODUCTID");
            entity.Property(e => e.Productname)
                .HasMaxLength(50)
                .HasColumnName("PRODUCTNAME");
        });

        modelBuilder.Entity<Viewproductinventory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEWPRODUCTINVENTORY");

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Price)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("PRICE");
            entity.Property(e => e.Productid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PRODUCTID");
            entity.Property(e => e.Productname)
                .HasMaxLength(50)
                .HasColumnName("PRODUCTNAME");
            entity.Property(e => e.Quantity)
                .HasColumnType("NUMBER")
                .HasColumnName("QUANTITY");
            entity.Property(e => e.Updatedate)
                .HasColumnType("DATE")
                .HasColumnName("UPDATEDATE");
        });

        modelBuilder.Entity<Viewpurchaseordersummary>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEWPURCHASEORDERSUMMARY");

            entity.Property(e => e.Orderdate)
                .HasColumnType("DATE")
                .HasColumnName("ORDERDATE");
            entity.Property(e => e.Purchaseorderid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PURCHASEORDERID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("STATUS");
            entity.Property(e => e.Supplierid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("SUPPLIERID");
            entity.Property(e => e.Suppliername)
                .HasMaxLength(50)
                .HasColumnName("SUPPLIERNAME");
        });

        modelBuilder.Entity<Viewsalesummary>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEWSALESUMMARY");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Clientid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CLIENTID");
            entity.Property(e => e.Saledate)
                .HasColumnType("DATE")
                .HasColumnName("SALEDATE");
            entity.Property(e => e.Saleid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("SALEID");
            entity.Property(e => e.Totalsale)
                .HasColumnType("NUMBER")
                .HasColumnName("TOTALSALE");
        });

        modelBuilder.Entity<Viewvaccineinfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEWVACCINEINFO");

            entity.Property(e => e.Applicationdate)
                .HasColumnType("DATE")
                .HasColumnName("APPLICATIONDATE");
            entity.Property(e => e.Appliedvaccineid)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("APPLIEDVACCINEID");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Petname)
                .HasMaxLength(50)
                .HasColumnName("PETNAME");
            entity.Property(e => e.Vaccinename)
                .HasMaxLength(50)
                .HasColumnName("VACCINENAME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
