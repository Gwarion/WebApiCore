using FluentValidation;

namespace PlaceHolder.Application.Logic.Commands.Consumers
{
    public class UpdateConsumerCommandValidator : AbstractValidator<UpdateConsumerCommand>
    {
        public UpdateConsumerCommandValidator()
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

            RuleFor(command => command.Guid)
                .NotNull()
                .WithMessage(command => $"{nameof(command.Guid)} must not be null")
                .Must(guid => guid != default)
                .WithMessage(command => $"{nameof(command.Guid)} must be set");
        }
    }
}
