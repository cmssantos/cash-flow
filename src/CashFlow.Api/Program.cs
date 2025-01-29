using System.Globalization;
using CashFlow.Api.Filters;
using CashFlow.Application.Interfaces;
using CashFlow.Application.Services;
using CashFlow.Domain.Interfaces;
using CashFlow.Infrastructure.Resources;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

builder.Services.AddSingleton<IResourceAccessor, JsonResourceAccessor>();
builder.Services.AddScoped<ILocalizer, Localizer>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

var app = builder.Build();

var supportedCultures = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("pt-BR")
};

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    RequestCultureProviders =
    [
        new AcceptLanguageHeaderRequestCultureProvider()
    ]
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
