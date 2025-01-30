using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.AutoMapper;


public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestRegisterExpenseJson, Domain.Entities.Expense>();
    }

    private void EntityToResponse()
    {
        CreateMap<Domain.Entities.Expense, ResponseRegisteredExpenseJson>();
    }
}
