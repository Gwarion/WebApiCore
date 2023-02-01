using FluentValidation;

namespace PlaceHolder.Application.Logic.Queries.Consumers
{

    public class GetOneConsumerByIdQueryValidator : AbstractValidator<GetOneConsumerByIdQuery>
    {
        public GetOneConsumerByIdQueryValidator()
        {
            RuleFor(command => command.Guid)
                .NotEmpty()
                .WithMessage(command => $"{nameof(command.Guid)} must be set");
        }
    }
}
