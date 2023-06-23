using FluentValidation.Results;

namespace Sigma.Domain.Interfaces
{
    /// <summary>
    /// Documentation API Validation Fields. 
    /// https://fluentvalidation.net
    /// </summary>

    public interface ISelfValidation
    {
        ValidationResult ValidationResult { get; }
        bool IsValid { get; }
    }
}
