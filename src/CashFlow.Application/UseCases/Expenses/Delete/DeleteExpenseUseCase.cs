using CashFlow.Application.Services;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Delete;

public class DeleteExpenseUseCase(IExpensesWriteOnlyRepository expenseRepository, IUnitOfWork unitOfWork,
    ILocalizer localizer) : IDeleteExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepository _expenseRepository = expenseRepository;
    private readonly ILocalizer _localizer = localizer;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(long id)
    {
        var result = await _expenseRepository.DeleteAsync(id);
        if (result is false)
        {
            throw new NotFoundException(_localizer.GetString("Error.NotFound"));
        }

        await _unitOfWork.CommitAsync();
    }
}
