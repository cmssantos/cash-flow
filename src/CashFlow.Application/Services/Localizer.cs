using CashFlow.Domain.Interfaces;

namespace CashFlow.Application.Services;

internal class Localizer(IResourceAccessor resourceAccessor) : ILocalizer
{
    private readonly IResourceAccessor _resourceAccessor = resourceAccessor;

    public string GetString(string key) => _resourceAccessor.GetString(key);
}
