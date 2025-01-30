using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository(CashFlowDbContext dbContext) : IExpensesRepository
{
    private CashFlowDbContext _dbContext { get; } = dbContext;

    public async Task AddAsync(Expense expense) => await _dbContext.Expenses.AddAsync(expense);
}
