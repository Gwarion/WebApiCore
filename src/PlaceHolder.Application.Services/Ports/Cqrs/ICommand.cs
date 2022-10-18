using MediatR;

namespace PlaceHolder.Application.Services.Ports.Cqrs
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<TResult> : IRequest<TResult>
    {
    }
}
