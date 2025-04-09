using backend.Domain.Entities;
using backend.Infrastructure.Persistence.Context.Configurations;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace backend.Infrastructure.Persistence.Context;

public partial class ApplicationDbContext : DbContext
{
    private readonly string _connectionString;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, string connectionString)
        : base(options)
    {
        _connectionString = connectionString;
    }

    public OracleConnection CreateConnection => new OracleConnection(_connectionString);

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("VETFRIENDS")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.ApplyConfiguration(new AppliedVaccinesConfiguration());
        modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
        modelBuilder.ApplyConfiguration(new AppointmentDetailConfiguration());
        modelBuilder.ApplyConfiguration(new AppUserConfiguration());
        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new InventoryConfiguration());
        modelBuilder.ApplyConfiguration(new MedicConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new PetConfiguration());
        modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductCategoryRelationConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new PurchaseOrderConfiguration());
        modelBuilder.ApplyConfiguration(new PurchaseOrderDetailConfiguration());
        modelBuilder.ApplyConfiguration(new SaleConfiguration());
        modelBuilder.ApplyConfiguration(new SaleDetailConfiguration());
        modelBuilder.ApplyConfiguration(new SupplierConfiguration());
        modelBuilder.ApplyConfiguration(new VaccineConfiguration());
        modelBuilder.ApplyConfiguration(new ViewActiveAppUserConfiguration());
        modelBuilder.ApplyConfiguration(new ViewAppointmentInfoConfiguration());
        modelBuilder.ApplyConfiguration(new ViewClientInfo());
        modelBuilder.ApplyConfiguration(new ViewPaymentSummaryConfiguration());
        modelBuilder.ApplyConfiguration(new ViewPetDetailConfiguration());
        modelBuilder.ApplyConfiguration(new ViewProductCategoryMappingConfiguration());
        modelBuilder.ApplyConfiguration(new ViewProductInventoryConfiguration());
        modelBuilder.ApplyConfiguration(new ViewPurchaseOrderSummaryConfiguration());
        modelBuilder.ApplyConfiguration(new ViewSaleSummaryConfiguration());
        modelBuilder.ApplyConfiguration(new ViewVaccineInfoConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}