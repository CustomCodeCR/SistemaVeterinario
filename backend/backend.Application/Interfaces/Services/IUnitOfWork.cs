// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Interfaces.Persistence;
using backend.Domain.Entities;
using System.Data;

namespace backend.Application.Interfaces.Services;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Appuser> User { get; }
    IGenericRepository<Client> Client { get; }
    IGenericRepository<Medic> Medic { get; }
    IGenericRepository<Pet> Pet { get; }
    IGenericRepository<Vaccine> Vaccine { get; }
    IGenericRepository<Appliedvaccine> AppliedVaccine { get; }
    IGenericRepository<Inventory> Inventory { get; }
    IGenericRepository<Payment> Payment { get; }
    IGenericRepository<Product> Product { get; }
    IGenericRepository<Productcategory> ProductCategory { get; }
    IGenericRepository<Productcategoryrelation> ProductCategoryRelation { get; }
    IGenericRepository<Supplier> Supplier { get; }
    IGenericRepository<Appointment> Appointment { get; }
    IAppointmentDetailRepository AppointmentDetail { get; }
    IGenericRepository<Purchaseorder> PurchaseOrder { get; }
    IPurchaseOrderDetailRepository PurchaseOrderDetail { get; }
    Task SaveChangesAsync();
    IDbTransaction BeginTransaction();
}