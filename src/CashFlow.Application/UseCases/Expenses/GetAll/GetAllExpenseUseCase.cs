using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll;

public class GetAllExpenseUseCase(IExpensesReadOnlyRepository expenseRepository, IMapper mapper) : IGetAllExpenseUseCase
{
    private readonly IExpensesReadOnlyRepository _expenseRepository = expenseRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseExpensesJson> ExecuteAsync()
    {
        var result = await _expenseRepository.GetAsync();

        return new ResponseExpensesJson
        {
            Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(result)
        };
    }
}
