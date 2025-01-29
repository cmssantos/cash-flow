using CashFlow.Application.Interfaces;
using CashFlow.Domain.Interfaces;

namespace CashFlow.Application.Services;

public class Localizer(IResourceAccessor resourceAccessor) : ILocalizer
{
    private readonly IResourceAccessor _resourceAccessor = resourceAccessor;

    public string GetString(string key) => _resourceAccessor.GetString(key);
}
