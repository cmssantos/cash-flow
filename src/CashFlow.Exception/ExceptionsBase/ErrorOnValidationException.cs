namespace CashFlow.Exception.ExceptionsBase;

public class ErrorOnValidationException(List<string> errorMessages) : CashFlowException(string.Empty)
{
    public List<string> Errors { get; set; } = errorMessages;
}
