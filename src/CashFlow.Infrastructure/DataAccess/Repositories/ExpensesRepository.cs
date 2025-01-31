using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository(CashFlowDbContext dbContext) : IExpensesReadOnlyRepository,
    IExpensesWriteOnlyRepository, IExpensesUpdateOnlyRepository
{
    private CashFlowDbContext _dbContext { get; } = dbContext;

    public async Task AddAsync(Expense expense) => await _dbContext.Expenses.AddAsync(expense);

    public async Task<List<Expense>> GetAsync() => await _dbContext.Expenses.AsNoTracking().ToListAsync();

    async Task<Expense?> IExpensesReadOnlyRepository.GetAsync(long id) =>
        await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    async Task<Expense?> IExpensesUpdateOnlyRepository.GetAsync(long id) =>
        await _dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<bool> DeleteAsync(long id)
    {
        var expense = await _dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == id);
        if (expense is null)
        {
            return false;
        }

        _dbContext.Expenses.Remove(expense);

        return true;
    }

    public void Update(Expense expense) => _dbContext.Expenses.Update(expense);
}
