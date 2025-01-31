
using AutoMapper;
using CashFlow.Application.Services;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Update;

public class UpdateExpenseUseCase(IExpensesUpdateOnlyRepository expenseRepository, IUnitOfWork unitOfWork,
    ILocalizer localizer, IMapper mapper) : IUpdateExpenseUseCase
{
    private readonly IExpensesUpdateOnlyRepository _expenseRepository = expenseRepository;
    private readonly ILocalizer _localizer = localizer;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task ExecuteAsync(long id, RequestExpenseJson request)
    {
        Validade(request);

        var expense = await _expenseRepository.GetAsync(id) ??
            throw new NotFoundException(_localizer.GetString("Error.NotFound"));

        _mapper.Map(request, expense);

        _expenseRepository.Update(expense);

        await _unitOfWork.CommitAsync();
    }

    private void Validade(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator(_localizer);
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors
                .Select(error => error.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
