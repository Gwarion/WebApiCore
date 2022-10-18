using FluentValidation;

namespace PlaceHolder.Application.Logic.Commands.Consumers
{
    public class CreateConsumerCommandValidator : AbstractValidator<CreateConsumerCommand>
    {
        public CreateConsumerCommandValidator()
        {
            RuleFor(command => command.FirstName)
                .NotNull()
                .WithMessage(command => $"{nameof(command.FirstName)} must not be null")
                .MinimumLength(1)
                .WithMessage(command => $"{nameof(command.FirstName)} must not be empty");

            RuleFor(command => command.LastName)
                .NotNull()
                .WithMessage(command => $"{nameof(command.LastName)} must not be null")
                .MinimumLength(1)
                .WithMessage(command => $"{nameof(command.LastName)} must not be empty");
        }
    }
}


