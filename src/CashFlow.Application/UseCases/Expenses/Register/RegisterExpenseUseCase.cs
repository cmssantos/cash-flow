using AutoMapper;
using CashFlow.Application.Services;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

internal class RegisterExpenseUseCase(IUnitOfWork unitOfWork, ILocalizer localizer, IMapper mapper)
    : IRegisterExpenseUseCase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILocalizer _localizer = localizer;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseRegisteredExpenseJson> Execute(RequestRegisterExpenseJson request)
    {
        Validade(request);

        var entity = _mapper.Map<Expense>(request);

        await _unitOfWork.Expenses.AddAsync(entity);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<ResponseRegisteredExpenseJson>(entity);
    }

    private void Validade(RequestRegisterExpenseJson request)
    {
        var validator = new RegisterExpenseValidator(_localizer);
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
