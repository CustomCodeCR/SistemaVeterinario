using backend.Application.Interfaces.Persistence;
using backend.Application.Interfaces.Services;
using backend.Infrastructure.Persistence.Context;
using backend.Infrastructure.Persistence.Repositories;
using backend.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ApplicationDbContext>();

        //services.AddScoped<IUserRepository, ExamRepository>();

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }
}