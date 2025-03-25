// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Interfaces.Persistence;
using backend.Domain.Entities;
using backend.Infrastructure.Persistence.Context;

namespace backend.Infrastructure.Persistence.Repositories;

public class AppointmentDetailRepository : GenericRepository<Appointmentdetail>, IAppointmentDetailRepository
{
    private readonly ApplicationDbContext _context;

    public AppointmentDetailRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<IEnumerable<Appointmentdetail>> GetAppointmentDetailByAppointmentId(int id)
    {
        throw new NotImplementedException();
    }
}