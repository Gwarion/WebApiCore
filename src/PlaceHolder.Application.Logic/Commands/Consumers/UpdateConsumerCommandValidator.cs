using FluentValidation;

namespace PlaceHolder.Application.Logic.Commands.Consumers
{
    public class UpdateConsumerCommandValidator : AbstractValidator<UpdateConsumerCommand>
    {
        public UpdateConsumerCommandValidator()
        {
            RuleFor(command => command.FirstName)
                .NotEmpty()
                .WithMessage(command => $"{nameof(command.FirstName)} must be set");

            RuleFor(command => command.LastName)
                .NotEmpty()
                .WithMessage(command => $"{nameof(command.LastName)} must be set");

            RuleFor(command => command.Guid)
                .NotEmpty()
                .WithMessage(command => $"{nameof(command.Guid)} must be set")
                .Must(guid => guid != default)
                .WithMessage(command => $"{nameof(command.Guid)} must be set");
        }
    }
}
