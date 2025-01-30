using CashFlow.Application.AutoMapper;
using CashFlow.Application.Services;
using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddLocalization(services);
        AddUseCases(services);

        return services;
    }

    private static void AddLocalization(IServiceCollection services) =>
        services.AddSingleton<ILocalizer, Localizer>();

    private static void AddAutoMapper(IServiceCollection services) =>
        services.AddAutoMapper(typeof(AutoMapping).Assembly);

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
        services.AddScoped<IGetAllExpenseUseCase, GetAllExpenseUseCase>();
        services.AddScoped<IGetExpenseByIdUseCase, GetExpenseByIdUseCase>();
    }
}
