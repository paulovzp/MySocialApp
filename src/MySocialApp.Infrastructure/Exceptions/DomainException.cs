using FluentValidation.Results;

namespace MySocialApp.Infrastructure.Exceptions;

public class DomainException : Exception
{
    public IDictionary<string, string[]> errors { get; } = new Dictionary<string, string[]>();

    public DomainException() : base()
    {
    }

    public DomainException(string message)
        : base(message)
    {
    }

    public DomainException(string message, Exception inner)
        : base(message, inner)
    {
    }

    public DomainException(IDictionary<string, string[]> errors)
    {
        this.errors = errors;
    }

    public DomainException(List<ValidationFailure> validationFailures)
        : base("Error")
    {
        var propertyNames = validationFailures.Select(e => e.PropertyName).Distinct();

        foreach (var propertyName in propertyNames)
        {
            var propertyFailures = validationFailures
                .Where(e => e.PropertyName == propertyName)
                .Select(e => e.ErrorMessage)
                .ToArray();

            errors.Add(propertyName, propertyFailures);
        }
    }
}
