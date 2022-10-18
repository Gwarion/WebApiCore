using FluentValidation;

namespace PlaceHolder.Application.Services.Cqrs.Validators
{
    public abstract class AbstractRequestValidator<TRequest> : AbstractValidator<TRequest>
    {
    }
}
