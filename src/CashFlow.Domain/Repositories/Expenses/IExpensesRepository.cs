using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpensesRepository
{
    Task AddAsync(Expense expense);
    Task<List<Expense>> GetAllAsync();
    Task<Expense?> GetByIdAsync(long id);
}
