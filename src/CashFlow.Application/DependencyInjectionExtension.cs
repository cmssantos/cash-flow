using CashFlow.Application.Services;
using CashFlow.Application.UseCases.Expenses.Register;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ILocalizer, Localizer>();
        services.AddSingleton<IRegisterExpenseUseCase, RegisterExpenseUseCase>();

        return services;
    }
}
