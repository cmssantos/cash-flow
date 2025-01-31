using AutoMapper;
using CashFlow.Application.Services;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

internal class RegisterExpenseUseCase(IExpensesWriteOnlyRepository expensesRepository, IUnitOfWork unitOfWork,
    ILocalizer localizer, IMapper mapper) : IRegisterExpenseUseCase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILocalizer _localizer = localizer;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseRegisteredExpenseJson> ExecuteAsync(RequestExpenseJson request)
    {
        Validade(request);

        var entity = _mapper.Map<Expense>(request);

        await expensesRepository.AddAsync(entity);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<ResponseRegisteredExpenseJson>(entity);
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
