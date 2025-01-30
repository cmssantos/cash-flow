using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll;

public class GetAllExpenseUseCase(IExpensesRepository expenseRepository, IMapper mapper) : IGetAllExpenseUseCase
{
    private readonly IExpensesRepository _expenseRepository = expenseRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseExpensesJson> Execute()
    {
        var result = await _expenseRepository.GetAllAsync();

        return new ResponseExpensesJson
        {
            Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(result)
        };
    }
}
