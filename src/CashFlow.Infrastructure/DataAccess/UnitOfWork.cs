using CashFlow.Domain.Repositories;

namespace CashFlow.Infrastructure.DataAccess;

internal class UnitOfWork(CashFlowDbContext dbContext) : IUnitOfWork
{
    private readonly CashFlowDbContext _dbContext = dbContext;

    public async Task CommitAsync() => await _dbContext.SaveChangesAsync();
}
