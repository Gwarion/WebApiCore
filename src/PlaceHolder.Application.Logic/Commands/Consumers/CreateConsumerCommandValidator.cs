using FluentValidation;

namespace PlaceHolder.Application.Logic.Commands.Consumers
{
    public class CreateConsumerCommandValidator : AbstractValidator<CreateConsumerCommand>
    {
        public CreateConsumerCommandValidator()
        {
            RuleFor(command => command.FirstName)
                .NotEmpty()
                .WithMessage(command => $"{nameof(command.FirstName)} must be set");

            RuleFor(command => command.LastName)
                .NotEmpty()
                .WithMessage(command => $"{nameof(command.LastName)} must be set");
        }
    }
}


