using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository(CashFlowDbContext dbContext) : IExpensesRepository
{
    private CashFlowDbContext _dbContext { get; } = dbContext;

    public async Task AddAsync(Expense expense) => await _dbContext.Expenses.AddAsync(expense);

    public async Task<List<Expense>> GetAllAsync() => await _dbContext.Expenses.AsNoTracking().ToListAsync();

    public async Task<Expense?> GetByIdAsync(long id) =>
        await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
}
