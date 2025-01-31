using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpensesWriteOnlyRepository
{
    Task AddAsync(Expense expense);

    /// <summary>
    /// Deletes an entity by its id.
    /// </summary>
    /// <param name="id">The id of the entity to delete.</param>
    /// <returns><see langword="true"/> if the entity was deleted, <see langword="false"/> otherwise.</returns>
    Task<bool> DeleteAsync(long id);
}
