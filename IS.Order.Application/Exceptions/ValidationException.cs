using FluentValidation.Results;

namespace IS.Order.Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(ValidationResult validationResult)
    {
        ValidationErrors = new List<string>();
        foreach (var validationResultError in validationResult.Errors)
        {
            ValidationErrors.Add(validationResultError.ErrorMessage);
        }
    }

    public List<string> ValidationErrors { get; set; }
}
