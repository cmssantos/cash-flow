namespace Cms.CashFlow.Exception.ExceptionsBase;

public class ErrorOnValidationException : CashFlowException
{
    public List<string> Errors { get; set; }

    public ErrorOnValidationException(List<string> errors) => Errors = errors;
}
