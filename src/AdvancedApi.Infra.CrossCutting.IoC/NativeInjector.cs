using AdvancedApi.Application.Interfaces;
using AdvancedApi.Application.Services;
using AdvancedApi.Domain.Interfaces;
using AdvancedApi.Infra.Data.Context;
using AdvancedApi.Infra.Data.Repositories;
using AdvancedApi.Infra.Data.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace AdvancedApi.Infra.CrossCutting.IoC;

public static class NativeInjector
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<AdvancedContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserServices, UserServices>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}