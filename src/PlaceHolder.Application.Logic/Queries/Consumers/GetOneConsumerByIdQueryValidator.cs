using FluentValidation;

namespace PlaceHolder.Application.Logic.Queries.Consumers
{

    public class GetOneConsumerByIdQueryValidator : AbstractValidator<GetOneConsumerByIdQuery>
    {
        public GetOneConsumerByIdQueryValidator()
        {
            RuleFor(command => command.Guid)
                .NotNull()
                .WithMessage(command => $"{nameof(command.Guid)} must not be null")
                .Must(guid => guid != default)
                .WithMessage(command => $"{nameof(command.Guid)} must be set");
        }
    }
}
