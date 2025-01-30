using CashFlow.Domain.Interfaces;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using CashFlow.Infrastructure.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
        AddLocalization(services);

        return services;
    }

    private static void AddLocalization(IServiceCollection services) =>
        services.AddSingleton<IResourceAccessor, JsonResourceAccessor>();

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IExpensesRepository, ExpensesRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var serverVersion = new MySqlServerVersion(new Version(major: 8, minor: 4, build: 4));

        services.AddDbContext<CashFlowDbContext>(config =>
        {
            config.UseMySql(connectionString, serverVersion);
        });
    }
}
