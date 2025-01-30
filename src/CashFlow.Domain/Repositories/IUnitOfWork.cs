using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Domain.Repositories;

public interface IUnitOfWork
{
    IExpensesRepository Expenses { get; }
    void Commit();
}
