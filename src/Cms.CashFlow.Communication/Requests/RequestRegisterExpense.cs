using Cms.CashFlow.Communication.Enums;

namespace Cms.CashFlow.Communication.Requests;

public class RequestRegisterExpense
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}