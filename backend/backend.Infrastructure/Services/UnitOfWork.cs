// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Interfaces.Persistence;
using backend.Application.Interfaces.Services;
using backend.Domain.Entities;
using backend.Infrastructure.Persistence.Context;
using backend.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace backend.Infrastructure.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private bool _disposed = false;

    private IGenericRepository<Appuser> _user = null!;
    private IGenericRepository<Client> _client = null!;
    private IGenericRepository<Medic> _medic = null!;
    private IGenericRepository<Pet> _pet = null!;
    private IGenericRepository<Vaccine> _vaccine = null!;
    private IGenericRepository<Appliedvaccine> _appliedVaccine = null!;
    private IGenericRepository<Inventory> _inventory = null!;
    private IGenericRepository<Payment> _payment = null!;
    private IGenericRepository<Product> _product = null!;
    private IGenericRepository<Productcategory> _productCategory = null!;
    private IGenericRepository<Productcategoryrelation> _productCategoryRelation = null!;
    private IGenericRepository<Supplier> _supplier = null!;
    private IGenericRepository<Appointment> _appointment = null!;
    private IAppointmentDetailRepository _appointmentDetail = null!;
    private IGenericRepository<Purchaseorder> _purchaseOrder = null!;
    private IPurchaseOrderDetailRepository _purchaseOrderDetail = null!;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<Appuser> User => _user ?? new GenericRepository<Appuser>(_context);
    public IGenericRepository<Client> Client => _client ?? new GenericRepository<Client>(_context);
    public IGenericRepository<Medic> Medic => _medic ?? new GenericRepository<Medic>(_context);
    public IGenericRepository<Pet> Pet => _pet ?? new GenericRepository<Pet>(_context);
    public IGenericRepository<Vaccine> Vaccine => _vaccine ?? new GenericRepository<Vaccine>(_context);
    public IGenericRepository<Appliedvaccine> AppliedVaccine => _appliedVaccine ?? new GenericRepository<Appliedvaccine>(_context);
    public IGenericRepository<Inventory> Inventory => _inventory ?? new GenericRepository<Inventory>(_context);
    public IGenericRepository<Payment> Payment => _payment ?? new GenericRepository<Payment>(_context);
    public IGenericRepository<Product> Product => _product ?? new GenericRepository<Product>(_context);
    public IGenericRepository<Productcategory> ProductCategory => _productCategory ?? new GenericRepository<Productcategory>(_context);
    public IGenericRepository<Productcategoryrelation> ProductCategoryRelation => _productCategoryRelation ?? new GenericRepository<Productcategoryrelation>(_context);
    public IGenericRepository<Supplier> Supplier => _supplier ?? new GenericRepository<Supplier>(_context);
    public IGenericRepository<Appointment> Appointment => _appointment ?? new GenericRepository<Appointment>(_context);
    public IAppointmentDetailRepository AppointmentDetail => _appointmentDetail ?? new AppointmentDetailRepository(_context);
    public IGenericRepository<Purchaseorder> PurchaseOrder => _purchaseOrder ?? new GenericRepository<Purchaseorder>(_context);
    public IPurchaseOrderDetailRepository PurchaseOrderDetail => _purchaseOrderDetail ?? new PurchaseOrderDetailRepository(_context);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public IDbTransaction BeginTransaction()
    {
        var transaction = _context.Database.BeginTransaction();
        return transaction.GetDbTransaction();
    }
}