// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;

namespace backend.Application.Interfaces.Persistence;

public interface IAppointmentDetailRepository : IGenericRepository<Appointmentdetail> 
{
    Task<IEnumerable<Appointmentdetail>> GetAppointmentDetailByAppointmentId(int id);
}