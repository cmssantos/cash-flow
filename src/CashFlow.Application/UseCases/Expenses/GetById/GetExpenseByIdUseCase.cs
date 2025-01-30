using AutoMapper;
using CashFlow.Application.Services;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public class GetExpenseByIdUseCase(IExpensesRepository expenseRepository, IMapper mapper, ILocalizer localizer)
    : IGetExpenseByIdUseCase
{
    private readonly IExpensesRepository _expenseRepository = expenseRepository;
    private readonly ILocalizer _localizer = localizer;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseExpenseJson> Execute(long id)
    {
        var result = await _expenseRepository.GetByIdAsync(id);

        return result is null
            ? throw new NotFoundException(_localizer.GetString("Error.NotFound"))
            : _mapper.Map<ResponseExpenseJson>(result);
    }
}
