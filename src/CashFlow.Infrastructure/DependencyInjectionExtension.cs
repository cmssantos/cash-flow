using CashFlow.Domain.Interfaces;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess.Repositories;
using CashFlow.Infrastructure.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IResourceAccessor, JsonResourceAccessor>();
        services.AddScoped<IExpensesRepository, ExpensesRepository>();

        return services;
    }
}
